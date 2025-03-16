using System.Net.Mime;
using Backend.Contracts;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("products")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ProductController(IProductService productService, IUpdateProductService updateProductService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IUpdateProductService _updateProductService = updateProductService;

        // Criar um novo produto
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { publicId = createdProduct.PublicId }, createdProduct);
        }

        // Buscar todos os produtos
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return products.Any() ? Ok(products) : NoContent();
        }

        // Buscar um produto pelo PublicId
        [HttpGet("{publicId:guid}")]
        public async Task<IActionResult> GetProductById(Guid publicId)
        {
            var product = await _productService.GetProductByIdAsync(publicId);
            return Ok(product);
        }

        // Atualizar um produto pelo PublicId
        [HttpPatch("{publicId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(Guid publicId, [FromBody] UpdateProductDto productDto)
        {
            // O FluentValidation vai automaticamente validar o DTO antes de continuar a execução
            var updatedProduct = await _updateProductService.UpdateProductAsync(publicId, productDto);
            return Ok(updatedProduct); // Se passar na validação, retorna o produto atualizado
        }

        // Apagar um produto pelo PublicId
        [HttpDelete("{publicId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid publicId)
        {
            await _productService.DeleteProductAsync(publicId);
            return NoContent();
        }
    }
}