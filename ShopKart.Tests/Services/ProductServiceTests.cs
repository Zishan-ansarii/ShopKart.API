using Moq;
using ShopKart.API.DTOs;
using ShopKart.API.Models.Entities;
using ShopKart.API.Repositories.interfaces;
using ShopKart.API.Repositories.Interfaces;
using ShopKart.API.Services.Implementations;

namespace ShopKart.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();

            _mockUnitOfWork.Setup(u => u.Products)
                           .Returns(_mockProductRepo.Object);
            _mockUnitOfWork.Setup(u => u.Categories)
                           .Returns(_mockCategoryRepo.Object);

            _productService = new ProductService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllProducts_WhenProductsExists_ReturnsProductList()
        {
            //  ARRANGE
            var fakeProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Phone",
                    Description = "A test phone",
                    Price = 9999.00m,
                    Stock = 10,
                    ImageUrl = "/images/phone.jpg",
                    CategoryId = 1,
                    Category = new Category {Id = 1, Name = "Electronics"}
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Shirt",
                    Description = "A test shirt",
                    Price = 999.00m,
                    Stock = 50,
                    ImageUrl = "/images/shirt.jpg",
                    CategoryId = 2,
                    Category = new Category { Id = 2, Name = "Clothing" }
                }
            };

            _mockProductRepo.Setup(r => r.GetAllWithCategoryAsync())
                            .ReturnsAsync(fakeProducts);

            //  ACT
            var result = await _productService.GetAllProductsAsync();

            //  ASSERT
            var productList = result.ToList();
            Assert.NotNull(productList);
            Assert.Equal(2, productList.Count);
            Assert.Equal("Test Phone", productList[0].Name);
            Assert.Equal("Electronics", productList[0].CategoryName);
            Assert.Equal("Test Shirt", productList[1].Name);
            Assert.Equal("Clothing", productList[1].CategoryName);
        }

        [Fact]
        public async Task GetProductById_WhenProductNotFound_RetunsNull()
        {
            //  ARRANGE
            Product? product = null;
            _mockProductRepo.Setup(r => r.GetProductWithCategoryAsync(999))
                            .ReturnsAsync(product);
            //  ACT
            var result = await _productService.GetProductByIdAsync(999);

            //  ASSERT
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateProductAsync_ValidData_RetunsProductResponsDTO()
        {
            //  ARRANGE
            var dto = new ProductCreateDTO
            {
                Name = "Test IPhone 15",
                Description = "Test description",
                Price = 149999.00m,
                Stock = 30,
                ImageUrl = "images/iphone.jpg",
                CategoryId = 1
            };

            _mockCategoryRepo.Setup(c => c.ExistsAsync(dto.CategoryId))
                             .ReturnsAsync(true);

            _mockProductRepo.Setup(p => p.AddAsync(It.IsAny<Product>()))
                            .ReturnsAsync((Product product) =>
                            {
                                product.Id = 10;
                                return product;
                            });

            _mockUnitOfWork.Setup(u => u.SaveAsync())
                           .ReturnsAsync(1);

            //  ACT
            var result = await _productService.CreateProductAsync(dto);

            //  ASSERT
            Assert.NotNull(result);
            Assert.Equal(10, result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Price, result.Price);
            Assert.Equal(dto.CategoryId, result.CategoryId);

            _mockCategoryRepo.Verify(c => c.ExistsAsync(dto.CategoryId), Times.Once);
            _mockProductRepo.Verify(p => p.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProductAsync_CategoryIdNotExists_ThrowsKeyNotFoundException()
        {
            //  ARRANGE
            var dto = new ProductCreateDTO
            {
                Name = "Test IPhone 15",
                Description = "Test description",
                Price = 149999.00m,
                Stock = 30,
                ImageUrl = "images/iphone.jpg",
                CategoryId = 1
            };

            _mockCategoryRepo.Setup(c => c.ExistsAsync(dto.CategoryId))
                             .ReturnsAsync(false);

            // ACT & ASSERT
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.CreateProductAsync(dto));

            //  VERIFY
            _mockProductRepo.Verify(p => p.AddAsync(It.IsAny<Product>()), Times.Never);
            _mockUnitOfWork.Verify(i => i.SaveAsync(), Times.Never);
        }
    }
}
