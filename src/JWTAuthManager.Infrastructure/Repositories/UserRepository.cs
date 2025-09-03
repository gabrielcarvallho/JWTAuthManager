using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;
using JWTAuthManager.Infrastructure.Data;

namespace JWTAuthManager.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public IQueryable<User> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}