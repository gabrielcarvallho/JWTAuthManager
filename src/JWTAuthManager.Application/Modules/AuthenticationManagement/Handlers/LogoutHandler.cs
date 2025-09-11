using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class LogoutHandler : ICommandHandler<LogoutCommand, Result<bool>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAcessor;

    public LogoutHandler(
        IUnityOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        IUserService userService,
        IHttpContextAccessor httpContextAcessor)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _userService = userService;
        _httpContextAcessor = httpContextAcessor;
    }

    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserId();
        if (userId == null)
        {
            return Result<bool>.Failure("User not authenticated");
        }

        var token = await _unitOfWork.RefreshToken
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken, cancellationToken);

        if (token != null && !token.IsActive && token.UserId.ToString() == userId)
        {
            token.IsRevoked = true;
            _unitOfWork.RefreshToken.Update(token);
        }

        var accessToken = _httpContextAcessor.HttpContext?.Request.Headers["Authorization"]
            .FirstOrDefault()?.Replace("Bearer ", "");

        if (!String.IsNullOrEmpty(accessToken))
        {
            var blacklistedToken = _jwtTokenService.BlacklistTokenAsync(accessToken);
            _unitOfWork.BlacklistedToken.Add(blacklistedToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}