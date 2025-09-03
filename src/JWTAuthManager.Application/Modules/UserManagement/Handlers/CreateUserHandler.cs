using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnityOfWork unityOfWork, IUserService userService, IMapper mapper)
    {
        _unitOfWork = unityOfWork;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isEmailExists = await _unitOfWork.Users.ExistsAsync(u => u.Email == request.Email, cancellationToken);
            if (isEmailExists)
                return Result<UserDto>.Failure("A user with this email already exists");

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            user.PasswordHash = _userService.HashPassword(user, request.Password);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<UserDto>.Failure("An error occurred while creating the user");
        }
    }
}