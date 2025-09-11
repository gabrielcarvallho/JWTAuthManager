using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.DTOs;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

public class LoginCommand : ICommand<Result<AuthenticationDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}