using JWTAuthManager.Application.DTOs;

namespace JWTAuthManager.Application.Services;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(CreateUserDto request);
    Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto request);
    Task<bool> DeleteUserAsync(Guid id);
}