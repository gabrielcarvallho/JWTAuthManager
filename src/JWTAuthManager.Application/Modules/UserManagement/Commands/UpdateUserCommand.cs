using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Enums;

namespace JWTAuthManager.Application.Modules.UserManagement.Commands;

public class UpdateUserCommand : ICommand<Result<UserDto>>
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.ReadOnly;
    public bool IsAdmin { get; set; } = false;
    public bool IsActive { get; set; } = true;
}