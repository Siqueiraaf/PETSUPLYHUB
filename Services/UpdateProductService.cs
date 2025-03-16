using Backend.Contracts;
using Backend.Models;
using Backend.Repositories;
using FluentValidation;

namespace Backend.Services;

public class UpdateProductService : IUpdateProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<UpdateProductDto> _updateProductDtoValidator;
    public UpdateProductService(IProductRepository productRepository, IValidator<UpdateProductDto> updateProductDtoValidator)
    {
        _productRepository = productRepository;
        _updateProductDtoValidator = updateProductDtoValidator;
    }
    public async Task<Product> UpdateProductAsync(Guid publicId, UpdateProductDto productDto)
    {
        var existingProduct = await _productRepository.GetByPublicIdAsync(publicId);
        if (existingProduct == null)
            throw new KeyNotFoundException("Produto não encontrado.");
            
        // Validar o DTO de atualização
        var validationResult = await _updateProductDtoValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException($"Validation failed: {errors}");
        }

        // Atualizar os campos do produto conforme o DTO
        if (productDto.Name != null)
            existingProduct.Name = productDto.Name;
        if (productDto.Description != null)
            existingProduct.Description = productDto.Description;
        if (productDto.Category != null)
            existingProduct.Category = productDto.Category;
        if (productDto.AnimalSpecie != null)
            existingProduct.AnimalSpecie = productDto.AnimalSpecie;
        if (productDto.Price.HasValue)
            existingProduct.Price = productDto.Price.Value;
        // Atualizar o produto no repositório
        await _productRepository.UpdateProductAsync(existingProduct);
        return existingProduct;
    }
}

