using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Payment Update(Payment payment);
    Task<Payment?> GetPaymentByOrderIdAsync(string orderId);
}