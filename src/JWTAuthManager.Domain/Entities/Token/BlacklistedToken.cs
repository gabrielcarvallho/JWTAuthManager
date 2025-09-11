using JWTAuthManager.Domain.Entities.Common;

namespace JWTAuthManager.Domain.Entities.Token;

public class BlacklistedToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime BlacklistedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
}