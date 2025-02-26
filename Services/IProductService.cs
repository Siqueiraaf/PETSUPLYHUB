using Backend.Contracts;
using Backend.Models;

namespace Backend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(CreateProductDto productDto);
        Task<Product> GetProductByIdAsync(Guid publicId);
        Task<Product> UpdateProductAsync(Guid publicId, CreateProductDto productDto);
        Task DeleteProductAsync(Guid publicId);
    }
}