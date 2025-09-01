using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IUserService userService, IMapper mapper)
    {
        _userRepository = userRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
            return Result<UserDto>.Failure("A user with this email already exists");

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        user.PasswordHash = _userService.HashPassword(user, request.Password);

        await _userRepository.CreateAsync(user);
        var response = _mapper.Map<UserDto>(user);

        return Result<UserDto>.Success(response);
    }
}