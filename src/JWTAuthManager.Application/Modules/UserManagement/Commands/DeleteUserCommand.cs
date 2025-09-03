using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;

namespace JWTAuthManager.Application.Modules.UserManagement.Commands;

public class DeleteUserCommand : ICommand<Result<UserDto>>
{
    public Guid Id { get; set; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}