using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class PaymentService(IUnitOfWork unitOfWork) : IPaymentService
{
    public async Task<Result<Payment>> AddAsync(string orderId, decimal amount, Guid userId, string serviceName)
    {
        var payment = new Payment()
        {
            Amount = amount,
            CreationDate = DateTimeOffset.Now,
            OrderId = orderId,
            PaymentStatus = PaymentStatus.Created,
            UserId = userId,
            Name = serviceName
        };

        var result = await unitOfWork.PaymentRepository.AddAsync(payment);
        return Result<Payment>.Success(result);
    }

    public async Task<Result<Payment>> UpdateAsync(Payment payment)
    {
        var result = unitOfWork.PaymentRepository.Update(payment);
        await unitOfWork.SaveChangesAsync();
        return Result<Payment>.Success(result);
    }

    public async Task<Result<Payment>> GetPaymentByOrderIdAsync(string orderId)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<Payment>.Failure(new Error("Payment not found", "Payment not found"));
        return Result<Payment>.Success(payment);
    }
}