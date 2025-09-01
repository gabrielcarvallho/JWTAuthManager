using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthManager.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string hashPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}