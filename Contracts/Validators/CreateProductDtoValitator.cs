using Backend.Contracts.DTOs;
using FluentValidation;

namespace Backend.Contracts.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("O nome não pode conter apenas espaços.");
            RuleFor(product => product.Description)
                .Length(10, 500).WithMessage("A descrição deve ter entre 10 e 500 caracteres.");
            RuleFor(product => product.Category)
                .NotEmpty().WithMessage("A categoria é obrigatória.")
                .Matches(@"^[A-Z]").WithMessage("A categoria deve começar com uma letra maiúscula.");
            RuleFor(product => product.AnimalSpecie)
                .NotEmpty().WithMessage("A espécie do animal é obrigatória.");
            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
        }
    }   

}
