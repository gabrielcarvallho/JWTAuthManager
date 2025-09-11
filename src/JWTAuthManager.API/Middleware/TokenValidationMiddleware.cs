using JWTAuthManager.Application.Common.Interfaces.Services;

namespace JWTAuthManager.API.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJwtTokenService jwtTokenService)
    {
        var cancellationToken = context.RequestAborted;
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            var isBlacklisted = await jwtTokenService.IsTokenBlacklistedAsync(token, cancellationToken);
            if (isBlacklisted)
            {
                throw new UnauthorizedAccessException("Token has been revoked");
            }
        }

        await _next(context);
    }
}