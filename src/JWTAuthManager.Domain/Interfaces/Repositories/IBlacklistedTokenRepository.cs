using JWTAuthManager.Domain.Entities.Token;

namespace JWTAuthManager.Domain.Interfaces.Repositories;

public interface IBlacklistedTokenRepository : IRepository<BlacklistedToken>
{
}