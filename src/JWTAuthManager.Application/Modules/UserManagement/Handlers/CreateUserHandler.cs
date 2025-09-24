using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateUserHandler(
        IUnityOfWork unityOfWork,
        IPasswordHasher<User> passwordHasher, 
        IMapper mapper, 
        IEmailService emailService)
    {
        _unitOfWork = unityOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _unitOfWork.Users.ExistsAsync(u => u.Email == request.Email, cancellationToken))
                return Result<UserDto>.Failure("A user with this email already exists");

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            await _emailService.SendWelcomeEmailAsync(user.Email, fullName);

            var response = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<UserDto>.Failure("An error occurred while creating the user");
        }
    }
}