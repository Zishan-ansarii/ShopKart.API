using ShopKart.API.Models.Entities;
using System.Linq.Expressions;

namespace ShopKart.API.Repositories.interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //  READ Operations
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        //  WRITE Operations
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        //UTILITY
        Task<bool> ExistsAsync(int id);
        Task<int> CountAsync();
    }
}
