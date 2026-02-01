using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ReturnsProduct()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Test Product", SKU = "TEST-001", SellingPrice = 10.99m, CriticalStockLevel = 5 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
            Assert.Equal(expectedProduct.SKU, result.SKU);
            Assert.Equal(expectedProduct.SellingPrice, result.SellingPrice);
            Assert.Equal(expectedProduct.CriticalStockLevel, result.CriticalStockLevel);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var productId = 999;

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SearchProductsByNameAsync_WithValidName_ReturnsProducts()
        {
            // Arrange
            var productName = "Test";
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", SKU = "TEST-001", SellingPrice = 10.99m, CriticalStockLevel = 5 },
                new Product { Id = 2, Name = "Test Product 2", SKU = "TEST-002", SellingPrice = 15.99m, CriticalStockLevel = 10 }
            };

            _mockRepository.Setup(repo => repo.SearchByNameAsync(productName)).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.SearchProductsByNameAsync(productName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count());
        }

        [Fact]
        public async Task CreateProductAsync_WithValidProduct_CreatesAndReturnsProduct()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", SKU = "NEW-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };
            var createdProduct = new Product { Id = 1, Name = "New Product", SKU = "NEW-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(createdProduct);
            _mockRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.CreateProductAsync(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdProduct.Id, result.Id);
            Assert.Equal(newProduct.Name, result.Name);
            Assert.Equal(newProduct.SKU, result.SKU);
            Assert.Equal(newProduct.SellingPrice, result.SellingPrice);
            Assert.Equal(newProduct.CriticalStockLevel, result.CriticalStockLevel);
        }

        [Fact]
        public async Task CreateProductAsync_WithDuplicateSKU_ReturnsNull()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", SKU = "EXISTING-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };

            _mockRepository.Setup(repo => repo.AnyAsync(p => p.SKU == newProduct.SKU)).ReturnsAsync(true);

            // Act
            var result = await _productService.CreateProductAsync(newProduct);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidProduct_UpdatesAndReturnsProduct()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { Id = productId, Name = "Old Product", SKU = "OLD-001", SellingPrice = 10.99m, CriticalStockLevel = 5 };
            var updatedProduct = new Product { Name = "Updated Product", SKU = "OLD-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.UpdateProductAsync(productId, updatedProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingProduct.Id, result.Id);
            Assert.Equal(updatedProduct.Name, result.Name);
            Assert.Equal(updatedProduct.SKU, result.SKU);
            Assert.Equal(updatedProduct.SellingPrice, result.SellingPrice);
            Assert.Equal(updatedProduct.CriticalStockLevel, result.CriticalStockLevel);
        }

        [Fact]
        public async Task UpdateProductAsync_WithNonExistentProduct_ReturnsNull()
        {
            // Arrange
            var productId = 999;
            var updatedProduct = new Product { Name = "Updated Product", SKU = "NEW-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.UpdateProductAsync(productId, updatedProduct);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProductAsync_WithDuplicateSKU_ReturnsNull()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { Id = productId, Name = "Old Product", SKU = "OLD-001", SellingPrice = 10.99m, CriticalStockLevel = 5 };
            var updatedProduct = new Product { Name = "Updated Product", SKU = "DUPLICATE-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockRepository.Setup(repo => repo.AnyAsync(p => p.SKU == updatedProduct.SKU && p.Id != productId)).ReturnsAsync(true);

            // Act
            var result = await _productService.UpdateProductAsync(productId, updatedProduct);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProductAsync_WithValidId_DeletesProduct()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { Id = productId, Name = "Test Product", SKU = "TEST-001", SellingPrice = 10.99m, CriticalStockLevel = 5 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockRepository.Setup(repo => repo.RemoveAsync(existingProduct)).Returns(Task.CompletedTask);
            _mockRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductAsync_WithNonExistentProduct_ReturnsFalse()
        {
            // Arrange
            var productId = 999;

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsSkuUniqueAsync_WithUniqueSKU_ReturnsTrue()
        {
            // Arrange
            var sku = "UNIQUE-001";

            _mockRepository.Setup(repo => repo.AnyAsync(p => p.SKU == sku)).ReturnsAsync(false);

            // Act
            var result = await _productService.IsSkuUniqueAsync(sku);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsSkuUniqueAsync_WithDuplicateSKU_ReturnsFalse()
        {
            // Arrange
            var sku = "DUPLICATE-001";

            _mockRepository.Setup(repo => repo.AnyAsync(p => p.SKU == sku)).ReturnsAsync(true);

            // Act
            var result = await _productService.IsSkuUniqueAsync(sku);

            // Assert
            Assert.False(result);
        }
    }
}