using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DriveSalez.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed = false;
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    
    public IUserRepository UserRepository { get; }
    public IOneTimePurchaseRepository OneTimePurchaseRepository { get; }
    public IPaymentRepository PaymentRepository { get; }
    public ISubscriptionRepository SubscriptionRepository { get; }
    public IUserLimitRepository UserLimitRepository { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task BeginTransactionAsync()
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No transaction in progress.");
        await _currentTransaction.CommitAsync();
        await _currentTransaction.DisposeAsync();
        _currentTransaction = null;
    }

    public async Task RollbackTransactionAsync()
    { 
        if (_currentTransaction == null)
            throw new InvalidOperationException("No transaction in progress.");
        await _currentTransaction.RollbackAsync();
        await _currentTransaction.DisposeAsync();
        _currentTransaction = null;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            await DisposeAsyncCore();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    { 
        await _context.DisposeAsync();
    }
}