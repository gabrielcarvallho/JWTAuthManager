using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Modules.AuthenticationManagement.DTOs;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

public class RefreshTokenCommand : ICommand<Result<AuthenticationDto>>
{
    public string RefreshToken { get; set; } = string.Empty;
}