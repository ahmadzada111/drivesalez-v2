using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment)
    {
        var entry = await context.Payments.AddAsync(payment);
        return entry.Entity;
    }

    public Payment Update(Payment payment)
    {
        return context.Payments.Update(payment).Entity;
    }

    public async Task<Payment?> GetPaymentByOrderIdAsync(string orderId)
    {
        return await context.Payments.FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}