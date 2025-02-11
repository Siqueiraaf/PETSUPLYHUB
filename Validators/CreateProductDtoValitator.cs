using Backend.Contracts;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateProductDtoValitator : AbstractValidator<CreateProductDto>
    {
        // Valida os campos do DTO CreateProductDto usando FluentValidation para garantir que os dados sejam corretos
        public CreateProductDtoValitator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Nome não pode ser nulo.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");
            
            RuleFor(product => product.Description)
                .Length(10, 500).WithMessage("Descricão deve ter entre 3 e 1000 caracteres.");
            
            RuleFor(product => product.Category)
                .NotEmpty().WithMessage("Categoria não pode ser nulo.");
            
            RuleFor(product => product.AnimalSpecie)
                .NotEmpty().WithMessage("Especie do animal não pode ser nulo.");
            
            RuleFor(product => product.Price)
                .GreaterThan(0).NotEmpty().WithMessage("Preço não pode ser nulo.");
        }
    }
}