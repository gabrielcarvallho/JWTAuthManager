namespace JWTAuthManager.Application.Modules.AuthenticationManagement.DTOs;

public class AuthenticationDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}