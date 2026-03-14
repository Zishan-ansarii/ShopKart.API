using Microsoft.AspNetCore.Mvc;
using ShopKart.API.DTOs;
using ShopKart.API.Services.Interfaces;

namespace ShopKart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _service.GetProductsByCategoryIdAsync(categoryId);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDTO dto)
        {
            var product = await _service.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDTO dto)
        {
            var updatedProduct = await _service.UpdateProductAsync(id, dto);
            if (updatedProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _service.DeleteProductAsync(id);
            if (!isDeleted)
                return NotFound($"Product with ID {id} not found.");
            return NoContent();
        }
    }

}
