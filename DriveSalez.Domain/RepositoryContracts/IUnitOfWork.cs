namespace DriveSalez.Domain.RepositoryContracts;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    IUserRepository UserRepository { get; }
    IOneTimePurchaseRepository OneTimePurchaseRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    ISubscriptionRepository SubscriptionRepository { get; }
    IUserLimitRepository UserLimitRepository { get; }
    IUserSubscriptionRepository UserSubscriptionRepository { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}