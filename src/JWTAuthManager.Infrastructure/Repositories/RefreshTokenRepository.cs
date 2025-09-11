using JWTAuthManager.Domain.Entities.Token;
using JWTAuthManager.Domain.Interfaces.Repositories;
using JWTAuthManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthManager.Infrastructure.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
    }
}