using Backend.Contracts.DTOs;
using FluentValidation;

namespace Backend.Contracts.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo.")
                .When(x => x.Price.HasValue);
            RuleFor(x => x.Category)
                .MaximumLength(50).WithMessage("A categoria deve ter no máximo 50 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Category));
        }
    }
}