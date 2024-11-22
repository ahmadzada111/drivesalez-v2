using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IPaymentService
{
    Task<Result<Payment>> AddAsync(string orderId, decimal amount, Guid userId, string serviceName);
    Task<Result<Payment>> UpdateStatusAsync(Payment payment, PaymentStatus status);
    Task<Result<Payment>> GetPaymentByOrderIdAsync(string orderId);
}