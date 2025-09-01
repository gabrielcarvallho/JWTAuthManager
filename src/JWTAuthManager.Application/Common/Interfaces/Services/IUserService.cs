using JWTAuthManager.Domain.Entities;

namespace JWTAuthManager.Application.Common.Interfaces.Services;

public interface IUserService
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password, string providedPassword);
}