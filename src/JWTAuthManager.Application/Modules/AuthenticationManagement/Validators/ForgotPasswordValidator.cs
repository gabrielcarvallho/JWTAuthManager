using FluentValidation;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Validators;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
    }
}