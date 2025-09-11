using JWTAuthManager.Domain.Entities.Common;

namespace JWTAuthManager.Domain.Entities.Token;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
}