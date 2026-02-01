using Microsoft.AspNetCore.Mvc;
using MiniiERP1.Controllers;
using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _productsController = new ProductsController(_mockProductService.Object, new LoggingService());
        }

        [Fact]
        public async Task GetProduct_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Test Product", SKU = "TEST-001", SellingPrice = 10.99m, CriticalStockLevel = 5 };

            _mockProductService.Setup(service => service.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productsController.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(expectedProduct.Id, returnedProduct.Id);
            Assert.Equal(expectedProduct.Name, returnedProduct.Name);
            Assert.Equal(expectedProduct.SKU, returnedProduct.SKU);
            Assert.Equal(expectedProduct.SellingPrice, returnedProduct.SellingPrice);
            Assert.Equal(expectedProduct.CriticalStockLevel, returnedProduct.CriticalStockLevel);
        }

        [Fact]
        public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var productId = 999;

            _mockProductService.Setup(service => service.GetProductByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productsController.GetProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task SearchProducts_WithValidName_ReturnsOkResult()
        {
            // Arrange
            var productName = "Test";
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", SKU = "TEST-001", SellingPrice = 10.99m, CriticalStockLevel = 5 },
                new Product { Id = 2, Name = "Test Product 2", SKU = "TEST-002", SellingPrice = 15.99m, CriticalStockLevel = 10 }
            };

            _mockProductService.Setup(service => service.SearchProductsByNameAsync(productName)).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productsController.SearchProducts(productName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(expectedProducts.Count, returnedProducts.Count());
        }

        [Fact]
        public async Task SearchProducts_WithEmptyName_ReturnsBadRequestResult()
        {
            // Act
            var result = await _productsController.SearchProducts(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProduct_WithValidProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", SKU = "NEW-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };
            var createdProduct = new Product { Id = 1, Name = "New Product", SKU = "NEW-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };

            _mockProductService.Setup(service => service.CreateProductAsync(newProduct)).ReturnsAsync(createdProduct);

            // Act
            var result = await _productsController.CreateProduct(newProduct);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProduct = Assert.IsType<Product>(createdAtResult.Value);
            Assert.Equal(createdProduct.Id, returnedProduct.Id);
            Assert.Equal(newProduct.Name, returnedProduct.Name);
            Assert.Equal(newProduct.SKU, returnedProduct.SKU);
            Assert.Equal(newProduct.SellingPrice, returnedProduct.SellingPrice);
            Assert.Equal(newProduct.CriticalStockLevel, returnedProduct.CriticalStockLevel);
        }

        [Fact]
        public async Task CreateProduct_WithInvalidProduct_ReturnsBadRequestResult()
        {
            // Arrange
            var newProduct = new Product { Name = "", SKU = "", SellingPrice = -10m, CriticalStockLevel = -5 };
            var validationError = new Dictionary<string, string[]> { { "Product name is required", new[] { "Product name is required" } } };

            _mockProductService.Setup(service => service.CreateProductAsync(newProduct)).ReturnsAsync((Product)null);

            // Act
            var result = await _productsController.CreateProduct(newProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProduct_WithDuplicateSKU_ReturnsBadRequestResult()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", SKU = "DUPLICATE-001", SellingPrice = 20.99m, CriticalStockLevel = 15 };

            _mockProductService.Setup(service => service.CreateProductAsync(newProduct)).ReturnsAsync((Product)null);

            // Act
            var result = await _productsController.CreateProduct(newProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_WithValidProduct_ReturnsOkResult()
        {
            // Arrange
            var productId = 1;
            var updatedProduct = new Product { Name = "Updated Product", SKU = "UPDATED-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };
            var returnedProduct = new Product { Id = productId, Name = "Updated Product", SKU = "UPDATED-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockProductService.Setup(service => service.UpdateProductAsync(productId, updatedProduct)).ReturnsAsync(returnedProduct);

            // Act
            var result = await _productsController.UpdateProduct(productId, updatedProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUpdatedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(returnedProduct.Id, returnedUpdatedProduct.Id);
            Assert.Equal(updatedProduct.Name, returnedUpdatedProduct.Name);
            Assert.Equal(updatedProduct.SKU, returnedUpdatedProduct.SKU);
            Assert.Equal(updatedProduct.SellingPrice, returnedUpdatedProduct.SellingPrice);
            Assert.Equal(updatedProduct.CriticalStockLevel, returnedUpdatedProduct.CriticalStockLevel);
        }

        [Fact]
        public async Task UpdateProduct_WithNonExistentProduct_ReturnsNotFoundResult()
        {
            // Arrange
            var productId = 999;
            var updatedProduct = new Product { Name = "Updated Product", SKU = "UPDATED-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockProductService.Setup(service => service.UpdateProductAsync(productId, updatedProduct)).ReturnsAsync((Product)null);

            // Act
            var result = await _productsController.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_WithDuplicateSKU_ReturnsBadRequestResult()
        {
            // Arrange
            var productId = 1;
            var updatedProduct = new Product { Name = "Updated Product", SKU = "DUPLICATE-001", SellingPrice = 15.99m, CriticalStockLevel = 10 };

            _mockProductService.Setup(service => service.UpdateProductAsync(productId, updatedProduct)).ReturnsAsync((Product)null);

            // Act
            var result = await _productsController.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var productId = 1;

            _mockProductService.Setup(service => service.DeleteProductAsync(productId)).ReturnsAsync(true);

            // Act
            var result = await _productsController.DeleteProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_WithNonExistentProduct_ReturnsNotFoundResult()
        {
            // Arrange
            var productId = 999;

            _mockProductService.Setup(service => service.DeleteProductAsync(productId)).ReturnsAsync(false);

            // Act
            var result = await _productsController.DeleteProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}