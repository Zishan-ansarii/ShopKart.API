using ShopKart.API.DTOs;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.Interfaces;
using ShopKart.API.Services.Interfaces;

namespace ShopKart.API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync()
        {
            // entity products 
            var products = await _unitOfWork.Products.GetAllWithCategoryAsync(); // with Include()

            // returns ProductResponseDTOs
            return products.Select(p => MapToResponseDTO(p));
        }

        public async Task<ProductResponseDTO?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetProductWithCategoryAsync(id);

            if (product == null)
                return null;

            return MapToResponseDTO(product);
        }

        public async Task<ProductResponseDTO> CreateProductAsync(ProductCreateDTO dto)
        {

            var isValidCategoryId = await _unitOfWork.Categories.ExistsAsync(dto.CategoryId);

            if (!isValidCategoryId)
            {
                throw new KeyNotFoundException("Invalid CategoryId");
            }

            // dto to product entity conversion
            var productEntity = new Product();  // empty product
            MapToEntity(dto, productEntity);

            // saving entity to db
            var product = await _unitOfWork.Products.AddAsync(productEntity);
            await _unitOfWork.SaveAsync();

            // entity to response dto conversion
            return MapToResponseDTO(product);
        }

        public async Task<ProductResponseDTO?> UpdateProductAsync(int id, ProductUpdateDTO dto)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);

            if (existingProduct == null)
                return null;

            var isValidCategoryId = await _unitOfWork.Categories.ExistsAsync(dto.CategoryId);

            if (!isValidCategoryId)
            {
                throw new KeyNotFoundException("Invalid CategoryId");
            }

            UpdateEntityWithDTO(dto, existingProduct);

            await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveAsync();

            return MapToResponseDTO(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product is null)
                return false;

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);

            var productResponseDTO = products.Select(c => MapToResponseDTO(c));

            return productResponseDTO;
        }

        //  helper methods
        private ProductResponseDTO MapToResponseDTO(Product product)
        {
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "No Category"
            };
        }

        private void MapToEntity(ProductCreateDTO dto, Product entity)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Stock = dto.Stock;
            entity.ImageUrl = dto.ImageUrl;
            entity.CategoryId = dto.CategoryId;

        }

        private void UpdateEntityWithDTO(ProductUpdateDTO updateDto, Product oldEntity)
        {
            oldEntity.Name = updateDto.Name;
            oldEntity.Description = updateDto.Description;
            oldEntity.Price = updateDto.Price;
            oldEntity.Stock = updateDto.Stock;
            oldEntity.ImageUrl = updateDto.ImageUrl;
            oldEntity.CategoryId = updateDto.CategoryId;

        }
    }
}
