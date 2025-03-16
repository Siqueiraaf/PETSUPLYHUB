using Backend.Contracts;
using FluentValidation;

namespace Backend.Validators;
public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required and should be valid.");
        RuleFor(x => x.Password)
            .NotEmpty().MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.");
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.");
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => role == "Admin" || role == "Cliente")
            .WithMessage("Role must be either Admin or Cliente.");
    }
}
