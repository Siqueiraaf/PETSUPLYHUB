using Backend.Models;

namespace Backend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(Guid uuid);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Guid guid, Product updateProduct);
        Task<bool> DeleteProductAsync(Guid guid);
    }
}