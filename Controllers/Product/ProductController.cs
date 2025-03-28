using System.Net.Mime;
using Backend.Contracts.DTOs;
using Backend.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Product;

[Route("products")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUpdateProductService _updateProductService;

    public ProductController(IProductService productService, IUpdateProductService updateProductService)
    {
        _productService = productService;
        _updateProductService = updateProductService;
    }

    /// <summary>
    /// Cria um novo produto (Apenas Admins).
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var createdProduct = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProductById), new { publicId = createdProduct.PublicId }, createdProduct);
    }

    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return products.Any() ? Ok(products) : NoContent();
    }

    /// <summary>
    /// Retorna um produto pelo PublicId.
    /// </summary>
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetProductById(Guid publicId)
    {
        var product = await _productService.GetProductByIdAsync(publicId);
        return product is not null ? Ok(product) : NotFound(new { Message = "Produto não encontrado" });
    }

    /// <summary>
    /// Atualiza um produto pelo PublicId (Apenas Admins).
    /// </summary>
    [HttpPatch("{publicId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateProduct(Guid publicId, [FromBody] UpdateProductDto productDto)
    {
        var updatedProduct = await _updateProductService.UpdateProductAsync(publicId, productDto);
        return updatedProduct is not null ? Ok(updatedProduct) : NotFound(new { Message = "Produto não encontrado" });
    }

    /// <summary>
    /// Deleta um produto pelo PublicId (Apenas Admins).
    /// </summary>
    [HttpDelete("{publicId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteProduct(Guid publicId)
    {
        await _productService.DeleteProductAsync(publicId);
        return NoContent();
    }
}
