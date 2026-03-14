using Microsoft.EntityFrameworkCore;
using ShopKart.API.Data;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.Implementations;

namespace ShopKart.API.Repositories.interfaces
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int categoryId)
        {
            return await _dbSet
                .Include(c => c.Products.Where(p => p.IsActive))
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.IsActive);
        }
    }
}
