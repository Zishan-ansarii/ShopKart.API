using Microsoft.AspNetCore.Mvc;
using ShopKart.API.DTOs;
using ShopKart.API.Services.Interfaces;

namespace ShopKart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category is null) 
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var category = await _service.GetCategoryWithProductsAsync(id);
            if (category is null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDTO dto)
        {
            var category = await _service.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDTO dto)
        {
            var updatedCategory = await _service.UpdateCategoryAsync(id, dto);
            if (updatedCategory is null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var isDeleted = await _service.DeleteCategoryAsync(id);
            if (!isDeleted)
                return NotFound($"Category with ID {id} not found.");
            return NoContent();
        }
    }
}
