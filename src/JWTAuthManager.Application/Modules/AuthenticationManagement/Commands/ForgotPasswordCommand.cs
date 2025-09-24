using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

public class ForgotPasswordCommand : ICommand<Result<bool>>
{
    public string Email { get; set; } = string.Empty;
}