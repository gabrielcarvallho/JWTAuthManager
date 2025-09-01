using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;

namespace JWTAuthManager.Application.Modules.UserManagement.Commands;

public class DeleteUserCommand : ICommand<Result>
{
    public Guid Id { get; set; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}