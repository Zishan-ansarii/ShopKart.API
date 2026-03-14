using ShopKart.API.Models.Entities;

namespace ShopKart.API.Repositories.interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Product specific queries 
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetOutOfStockProductsAsync();
        Task<Product?> GetProductWithCategoryAsync(int productId);
    }
}
