using Microsoft.EntityFrameworkCore;
using ShopKart.API.Data;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.interfaces;
using System.Linq.Expressions;

namespace ShopKart.API.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ===== READ OPERATIONS =====

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        // ===== WRITE OPERATIONS =====

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                // Soft Delete - IsActive = false (record stays in DB)
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        // ===== UTILITY =====

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet
                .AnyAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet
                .CountAsync(e => e.IsActive);
        }
    }

}
