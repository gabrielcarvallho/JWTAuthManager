using FluentValidation;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Validators;

public class LogoutValidator : AbstractValidator<LogoutCommand>
{
    public LogoutValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}