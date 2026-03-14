using ShopKart.API.DTOs;

namespace ShopKart.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryResponseDTO> CreateCategoryAsync(CategoryCreateDTO dto);
        Task<CategoryResponseDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO dto);
        Task<CategoryResponseDTO?> GetCategoryWithProductsAsync(int id);
        Task<bool> DeleteCategoryAsync(int id);

    }
}
