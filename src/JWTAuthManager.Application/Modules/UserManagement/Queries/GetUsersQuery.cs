using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;

namespace JWTAuthManager.Application.Modules.UserManagement.Queries;

public class GetUsersQuery : IQuery<Result<PaginatedList<UserDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IsActive { get; set; } = true;
}