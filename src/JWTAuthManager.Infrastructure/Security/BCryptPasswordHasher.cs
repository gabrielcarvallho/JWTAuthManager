using Microsoft.AspNetCore.Identity;

namespace JWTAuthManager.Infrastructure.Security;

public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private const int WorkFactor = 12;

    public string HashPassword(TUser user, string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        var valid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        if (!valid)
            return PasswordVerificationResult.Failed;

        return PasswordVerificationResult.Success;
    }
}