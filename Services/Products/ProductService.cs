using Backend.Contracts.DTOs;
using Backend.Models;
using Backend.Repositories;
using FluentValidation;

namespace Backend.Services.Products
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

        // Cria um novo produto
        public async Task<Product> CreateProductAsync(CreateProductDto dto)
        {
            // Validação do DTO
            var validationResult = await _createProductDtoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errors}");
            }

            // Converte o DTO para o modelo de produto
            var product = ConvertToModel(dto, Guid.NewGuid());

            // Adiciona o produto no repositório
            await _productRepository.AddProductAsync(product);

            return product;
        }

        // Converte o DTO para o modelo de produto
        private Product ConvertToModel(CreateProductDto dto, Guid publicId)
        {
            return new Product
            {
                PublicId = publicId,  // Novo PublicId para um produto novo
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                AnimalSpecie = dto.AnimalSpecie,
                Price = dto.Price
            };
        }

        // Obtém um produto pelo PublicId
        public async Task<Product> GetProductByIdAsync(Guid publicId)
        {
            var product = await _productRepository.GetByPublicIdAsync(publicId);
            return product ?? throw new KeyNotFoundException("Produto não encontrado.");
        }

        // Obtém todos os produtos
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        // Atualiza um produto existente
        public async Task<Product> UpdateProductAsync(Guid publicId, CreateProductDto productDto)
        {
            var existingProduct = await _productRepository.GetByPublicIdAsync(publicId);
            if (existingProduct == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            var validationResult = await _createProductDtoValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errors}");
            }

            var updatedProduct = ConvertToModel(productDto, publicId);
            await _productRepository.UpdateProductAsync(updatedProduct);
            return updatedProduct;
        }

        // Exclui um produto existente
        public async Task DeleteProductAsync(Guid publicId)
        {
            var existingProduct = await _productRepository.GetByPublicIdAsync(publicId);
            if (existingProduct == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            await _productRepository.DeleteProductAsync(existingProduct);
        }
    }
}
