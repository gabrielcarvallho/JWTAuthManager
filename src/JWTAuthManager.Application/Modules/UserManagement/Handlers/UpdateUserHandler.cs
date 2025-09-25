using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unitOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result<UserDto>.Failure("User not found");
        }

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.IsAdmin = request.IsAdmin;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(response);
    }
}