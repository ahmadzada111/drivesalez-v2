using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment)
    {
        var entry = await context.Payments.AddAsync(payment);
        return entry.Entity;
    }

    public Payment Update(Payment payment, PaymentStatus status)
    {
        payment.PaymentStatus = status;
        return context.Payments.Update(payment).Entity;
    }

    public async Task<Payment?> GetPaymentByOrderIdAsync(string orderId)
    {
        return await context.Payments.FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}