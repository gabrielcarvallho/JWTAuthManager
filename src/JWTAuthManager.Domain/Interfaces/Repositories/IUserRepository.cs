using JWTAuthManager.Domain.Entities;

namespace JWTAuthManager.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    IQueryable<User> GetQueryable();
}