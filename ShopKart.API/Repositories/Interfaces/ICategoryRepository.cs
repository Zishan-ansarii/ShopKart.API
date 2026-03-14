using ShopKart.API.Models.Entities;

namespace ShopKart.API.Repositories.interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    }
}
