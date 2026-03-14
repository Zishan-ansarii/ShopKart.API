using ShopKart.API.DTOs;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.Interfaces;
using ShopKart.API.Services.Interfaces;

namespace ShopKart.API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryResponseDTO> CreateCategoryAsync(CategoryCreateDTO dto)
        {
            var categoryEntity = new Category();

            MapToEntity(dto, categoryEntity);

            var savedCategory = await _unitOfWork.Categories.AddAsync(categoryEntity);
            await _unitOfWork.SaveAsync();

            return MapToResponseDTO(savedCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                return false;

            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            return categories.Select(c => MapToResponseDTO(c));
        }

        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                return null;

            return MapToResponseDTO(category);
        }

        public async Task<CategoryResponseDTO?> GetCategoryWithProductsAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetCategoryWithProductsAsync(id);

            if (category is null)
                return null;

            return MapToResponseDTO(category);
        }

        public async Task<CategoryResponseDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO dto)
        {
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id);

            if (existingCategory is null)
                return null;

            UpdateEntityWithDTO(dto, existingCategory);

            await _unitOfWork.Categories.UpdateAsync(existingCategory);
            await _unitOfWork.SaveAsync();

            return MapToResponseDTO(existingCategory);
        }

        // helper methods
        private CategoryResponseDTO MapToResponseDTO(Category entity)
        {
            return new CategoryResponseDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Products = entity.Products?.Select(p => new ProductResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId,
                    CategoryName = entity.Name
                }).ToList() ?? new List<ProductResponseDTO>()
            };
        }

        private void MapToEntity(CategoryCreateDTO dto, Category entity)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
        }

        private void UpdateEntityWithDTO(CategoryUpdateDTO dto, Category oldCategory)
        {
            oldCategory.Name = dto.Name;
            oldCategory.Description = dto.Description;
        }
    }
}
