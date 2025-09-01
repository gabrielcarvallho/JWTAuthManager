using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Domain.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(request.Id))
            return Result.Failure("User not found");

        await _userRepository.DeleteAsync(request.Id);
        return Result.Success("User deleted successfully");
    }
}