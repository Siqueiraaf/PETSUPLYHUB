using Backend.Models;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<Product?> GetByPublicIdAsync(Guid publicId);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product); 
    }
}