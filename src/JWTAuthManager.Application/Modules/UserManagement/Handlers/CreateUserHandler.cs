using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.Commands;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(
        IUnityOfWork unityOfWork,
        IPasswordHasher<User> passwordHasher, 
        IMapper mapper, 
        IEmailService emailService,
        IJwtTokenService jwtTokenService,
        ILogger<CreateUserHandler> logger)
    {
        _unitOfWork = unityOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _emailService = emailService;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Users.ExistsAsync(u => u.Email == request.Email, cancellationToken))
            return Result<UserDto>.Failure("A user with this email already exists");

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N").Substring(0, 8));

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            var token = _jwtTokenService.GeneratePasswordResetToken(user);

            await _emailService.SendWelcomeEmailAsync(user.Email, fullName, token);
            _logger.LogInformation("Welcome email sent successfully to user {Email}", user.Email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to send welcome email to {Email}. User creation completed successfully.", user.Email);
        }

        var response = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(response);
    }
}