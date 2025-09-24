using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

public class ResetPasswordCommand : ICommand<Result<bool>>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}