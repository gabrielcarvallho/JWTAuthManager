namespace JWTAuthManager.Domain.Interfaces.Repositories;

public interface IUnityOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshToken { get; }
    IBlacklistedTokenRepository BlacklistedToken { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}