using Backend.Contracts;
using Backend.Contracts.DTOs;
using Backend.Models;

namespace Backend.Services.Products;

public interface IUpdateProductService
{
    Task<Product> UpdateProductAsync(Guid publicId, UpdateProductDto productDto);
}
