using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;

namespace JWTAuthManager.Application.Modules.UserManagement.Queries;

public class GetUserByIdQuery : IQuery<Result<UserDto>>
{
    public Guid Id { get; }

    public GetUserByIdQuery(Guid userId)
    {
        Id = userId;
    }
}