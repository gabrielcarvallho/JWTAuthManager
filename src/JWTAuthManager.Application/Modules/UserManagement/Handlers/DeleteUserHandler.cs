using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteUserHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unitOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result<UserDto>.Failure("User not found");
        }

        var userDto = _mapper.Map<UserDto>(user);
        
        user.isActive = false;
        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<UserDto>.Success(userDto);
    }
}