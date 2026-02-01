using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniiERP1.Models;

namespace MiniiERP1.Tests.Integration
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetProduct_WithValidId_ReturnsProduct()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Product", jsonString);
        }

        [Fact]
        public async Task GetProduct_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SearchProducts_WithValidName_ReturnsProducts()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products/search/Test");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Product", jsonString);
        }

        [Fact]
        public async Task CreateProduct_WithValidProduct_CreatesProduct()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newProduct = new Product
            {
                Name = "Integration Test Product",
                SKU = "INT-001",
                SellingPrice = 25.99m,
                CriticalStockLevel = 20
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/products", newProduct);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Integration Test Product", jsonString);
        }

        [Fact]
        public async Task UpdateProduct_WithValidProduct_UpdatesProduct()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatedProduct = new Product
            {
                Name = "Updated Integration Test Product",
                SKU = "INT-001",
                SellingPrice = 30.99m,
                CriticalStockLevel = 25
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/products/1", updatedProduct);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Updated Integration Test Product", jsonString);
        }

        [Fact]
        public async Task DeleteProduct_WithValidId_DeletesProduct()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/products/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}