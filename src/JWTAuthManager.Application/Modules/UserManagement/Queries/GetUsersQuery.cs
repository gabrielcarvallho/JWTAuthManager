using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;

namespace JWTAuthManager.Application.Modules.UserManagement.Queries;

public class GetUsersQuery : IQuery<Result<IEnumerable<UserDto>>>
{
}