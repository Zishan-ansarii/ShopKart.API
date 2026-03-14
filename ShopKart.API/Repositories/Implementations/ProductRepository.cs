using Microsoft.EntityFrameworkCore;
using ShopKart.API.Data;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.interfaces;

namespace ShopKart.API.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _dbSet
                         .Include(p => p.Category)
                         .Where(p => p.IsActive)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                         .Include(p => p.Category)
                         .Where(p => p.CategoryId == categoryId && p.IsActive)
                         .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
        {
            return await _dbSet
                         .Where(p => p.Stock == 0 && p.IsActive)
                         .ToListAsync();
        }
        public async Task<Product?> GetProductWithCategoryAsync(int productId)
        {
            return await _dbSet
                         .Include(p => p.Category)
                         .FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);
        }

    }

}
