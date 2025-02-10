using Backend.Models;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByPublicIdAsync(Guid publicId);
        Task AddProductAsync(Product product);
    }
}
