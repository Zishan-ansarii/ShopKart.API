using ShopKart.API.DTOs;

namespace ShopKart.API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync();
        public Task<ProductResponseDTO?> GetProductByIdAsync(int id);
        public Task<ProductResponseDTO> CreateProductAsync(ProductCreateDTO dto);
        public Task<ProductResponseDTO?> UpdateProductAsync(int id, ProductUpdateDTO dto);
        public Task<bool> DeleteProductAsync(int id);
        public Task<IEnumerable<ProductResponseDTO>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
