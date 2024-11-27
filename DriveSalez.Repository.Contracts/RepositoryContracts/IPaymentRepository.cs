using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Payment Update(Payment payment);
    Task<Payment?> GetPaymentByOrderIdAsync(string orderId);
}