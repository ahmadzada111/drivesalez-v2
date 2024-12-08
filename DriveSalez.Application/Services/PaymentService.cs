using DriveSalez.Application.Abstractions.Payment.Factory;
using DriveSalez.Application.Dto.Payment;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.Common.Enums;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class PaymentService(
    IUnitOfWork unitOfWork,
    IPaymentStrategyFactory paymentStrategyFactory,
    IPayPalService payPalService) : IPaymentService
{
    public async Task<Result<string>> ConfirmPaymentAsync(string orderId)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var result = await payPalService.CapturePaymentAsync(orderId);
            if (!result.IsSuccess)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Result<string>.Failure(result.Error!);
            }
        
            var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
            if (payment is null)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Result<string>.Failure(PaymentErrors.NotFound);
            }
            
            payment.MarkAsCompleted();
            var modifiedPayment = unitOfWork.PaymentRepository.Update(payment);
            await unitOfWork.SaveChangesAsync();
        
            var paymentStrategy = paymentStrategyFactory.GetStrategy(modifiedPayment.PurchaseType);
            await paymentStrategy.HandlePostPaymentAsync(modifiedPayment.PaidServiceId, modifiedPayment.UserId);

            await unitOfWork.CommitTransactionAsync();
            return Result<string>.Success(orderId);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<Result<string>> CancelPaymentAsync(string orderId)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<string>.Failure(PaymentErrors.NotFound);
        
        payment.MarkAsVoided();
        unitOfWork.PaymentRepository.Update(payment);
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
            CreationDate = DateTimeOffset.UtcNow,
            OrderId =  paymentResult.Value!.OrderId,
            PaidServiceId = service.Id,
            PaymentStatus = PaymentStatus.Created,
            UserId = request.UserId,
            Name = service.Name
        };

        await unitOfWork.PaymentRepository.AddAsync(payment);
        await unitOfWork.SaveChangesAsync();
        return Result<PaymentResponse>.Success(paymentResult.Value);
    }

    public async Task<Result<GetPaymentResponse>> GetPaymentByOrderIdAsync(string orderId)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<GetPaymentResponse>.Failure(PaymentErrors.NotFound);
        return Result<GetPaymentResponse>.Success((GetPaymentResponse)payment);
    }
}