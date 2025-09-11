using JWTAuthManager.Domain.Entities.Token;
using JWTAuthManager.Domain.Interfaces.Repositories;
using JWTAuthManager.Infrastructure.Data;

namespace JWTAuthManager.Infrastructure.Repositories;

public class BlacklistedTokenRepository : Repository<BlacklistedToken>, IBlacklistedTokenRepository
{
    public BlacklistedTokenRepository(AppDbContext context) : base(context)
    {
    }
}