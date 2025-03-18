using Backend.Contracts.DTOs;
using FluentValidation;

namespace Backend.Contracts.Validators
{
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
}
