using DriveSalez.Application.Abstractions.Payment.Factory;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.Payment;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class PaymentService(
    IUnitOfWork unitOfWork,
    IPaymentStrategyFactory paymentStrategyFactory,
    IPayPalService payPalService) : IPaymentService
{
    public async Task<Result<string>> ConfirmPaymentAsync(string orderId)
    {
        var result = await payPalService.CapturePaymentAsync(orderId);
        if (!result.IsSuccess) return Result<string>.Failure(result.Error!);
        
        var payment = await GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return Result<string>.Failure(payment.Error!);
        
        payment.Value!.PaymentStatus = PaymentStatus.Completed;
        var modifiedPayment = unitOfWork.PaymentRepository.Update(payment.Value!);
        await unitOfWork.SaveChangesAsync();
        
        var paymentStrategy = paymentStrategyFactory.GetStrategy(modifiedPayment.PurchaseType);
        await paymentStrategy.HandlePostPaymentAsync(modifiedPayment.PaidServiceId, modifiedPayment.UserId);

        return Result<string>.Success(orderId);
    }

    public async Task<Result<string>> CancelPaymentAsync(string orderId)
    {
        var payment = await GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return Result<string>.Failure(payment.Error!);
        
        payment.Value!.PaymentStatus = PaymentStatus.Voided;
        unitOfWork.PaymentRepository.Update(payment.Value!);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Success(orderId);
    }
    
    public async Task<Result<PaymentResponse>> InitiatePaymentAsync(PaymentInitiationRequest request, PaymentDetails paymentDetails)
    {
        var purchaseType = Enum.Parse<PurchaseType>(request.PurchaseType, true);
        var paymentStrategy = paymentStrategyFactory.GetStrategy(purchaseType);
        var service = await paymentStrategy.GetService(request.ServiceId);
        var paymentResult = await payPalService.ProcessPaymentAsync(paymentDetails, service.Amount);
        if (!paymentResult.IsSuccess) return Result<PaymentResponse>.Failure(paymentResult.Error!);
        
        var payment = new Payment()
        {
            Amount = service.Amount,
            CreationDate = DateTimeOffset.Now,
            OrderId =  paymentResult.Value!.OrderId,
            PaymentStatus = PaymentStatus.Created,
            UserId = request.UserId,
            Name = service.Name
        };

        await unitOfWork.PaymentRepository.AddAsync(payment);
        return Result<PaymentResponse>.Success(paymentResult.Value);
    }

    public async Task<Result<Payment>> GetPaymentByOrderIdAsync(string orderId)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<Payment>.Failure(new Error("Payment not found", "Payment not found"));
        return Result<Payment>.Success(payment);
    }
}