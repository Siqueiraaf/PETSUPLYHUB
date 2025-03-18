using Backend.Contracts.DTOs;
using FluentValidation;

namespace Backend.Contracts.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
            {
                RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("O nome completo é obrigatório.")
                    .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("O e-mail é obrigatório.")
                    .EmailAddress().WithMessage("Formato de e-mail inválido.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("A senha é obrigatória.")
                    .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
            }
    }
}
