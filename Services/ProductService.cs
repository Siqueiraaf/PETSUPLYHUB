using Backend.Contracts;
using Backend.Models;
using Backend.Repositories;
using FluentValidation;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductDto> _createProductDtoValidator;

        public ProductService(IProductRepository productRepository, IValidator<CreateProductDto> createProductDtoValidator)
        {
            _productRepository = productRepository;
            _createProductDtoValidator = createProductDtoValidator;
        }

        // Converte o DTO para a entidade Product
        private Product ConvertToModel(CreateProductDto dto)
        {
            return new Product
            {
                PublicId = dto.PublicId,
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                AnimalSpecie = dto.AnimalSpecie,
                Price = dto.Price
            };
        }

        // Cria um produto e persiste no banco de dados
        public async Task<Product> CreateProductAsync(CreateProductDto dto)
        {
            var validationResult = await _createProductDtoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errors}");
            }

            var product = ConvertToModel(dto);
            await _productRepository.AddProductAsync(product);
            return product;
        }

        // Obtém um produto pelo PublicId
        public async Task<Product> GetProductByIdAsync(Guid publicId)
        {
            var product = await _productRepository.GetByPublicIdAsync(publicId);
            return product ?? throw new KeyNotFoundException("Product not found.");
        }

        // Obtém todos os produtos
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
