using Backend.Contracts;
using Backend.Models;

namespace Backend.Services;

public interface IUpdateProductService
{
    Task<Product> UpdateProductAsync(Guid publicId, UpdateProductDto productDto);
}
