using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DriveSalez.Persistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IOneTimePurchaseRepository> _oneTimePurchaseRepository;
    private readonly Lazy<IPaymentRepository> _paymentRepository;
    private readonly Lazy<ISubscriptionRepository> _subscriptionRepository;
    private readonly Lazy<IUserLimitRepository> _userLimitRepository;
    private readonly Lazy<IUserSubscriptionRepository> _userSubscriptionRepository;
    
    public IUserRepository UserRepository => _userRepository.Value;
    public IOneTimePurchaseRepository OneTimePurchaseRepository => _oneTimePurchaseRepository.Value;
    public IPaymentRepository PaymentRepository => _paymentRepository.Value;
    public ISubscriptionRepository SubscriptionRepository => _subscriptionRepository.Value;
    public IUserLimitRepository UserLimitRepository => _userLimitRepository.Value;
    public IUserSubscriptionRepository UserSubscriptionRepository => _userSubscriptionRepository.Value;

    public UnitOfWork(ApplicationDbContext context, Lazy<IUserRepository> userRepository1, 
        Lazy<IOneTimePurchaseRepository> oneTimePurchaseRepository, Lazy<IPaymentRepository> paymentRepository, 
        Lazy<ISubscriptionRepository> subscriptionRepository, Lazy<IUserLimitRepository> userLimitRepository,
        Lazy<IUserSubscriptionRepository> userSubscriptionRepository)
    {
        _context = context;
        _userRepository = userRepository1;
        _oneTimePurchaseRepository = oneTimePurchaseRepository;
        _paymentRepository = paymentRepository;
        _subscriptionRepository = subscriptionRepository;
        _userLimitRepository = userLimitRepository;
        _userSubscriptionRepository = userSubscriptionRepository;
    }
    
    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) 
            throw new InvalidOperationException("A transaction has already been started.");
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

    private void Dispose(bool disposing)
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

    private async ValueTask DisposeAsyncCore()
    { 
        await _context.DisposeAsync();
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}