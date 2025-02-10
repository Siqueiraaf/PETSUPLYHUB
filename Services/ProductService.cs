using Backend.Contracts;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Converte o DTO para a entidade Product
        public Product ConvertToModel(CreateProductDto dto)
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
            var product = ConvertToModel(dto);

            // Adiciona o produto ao repositório
            await _productRepository.AddProductAsync(product);

            return product;
        }

        // Obtém um produto pelo PublicId
        public async Task<Product> GetProductByIdAsync(Guid publicId)
        {
            return await _productRepository.GetByPublicIdAsync(publicId);
        }

        // Obtém todos os produtos
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
