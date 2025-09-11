using JWTAuthManager.Domain.Entities.Common;

namespace JWTAuthManager.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool isAdmin { get; set; } = false;
    public bool isActive { get; set; } = true;
    public DateTime? LastLogin { get; set; }
    public string? PasswordHash { get; set; }
}