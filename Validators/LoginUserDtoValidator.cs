using Backend.Contracts;
using FluentValidation;

namespace Backend.Validators;
public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email é obrigatório e deve ser válido.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória.");
    }
}
