using ShopKart.API.Repositories.interfaces;

namespace ShopKart.API.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveAsync();
    }
}
