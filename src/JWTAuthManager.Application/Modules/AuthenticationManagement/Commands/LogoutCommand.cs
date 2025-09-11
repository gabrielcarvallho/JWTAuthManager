using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

public class LogoutCommand : ICommand<Result<bool>> 
{
    public string? RefreshToken { get; set; }
}