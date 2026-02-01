using Microsoft.AspNetCore.Mvc.Testing;
using MiniiERP1.Models;

namespace MiniiERP1.Tests.Integration
{
    public class InventoryControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public InventoryControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetInventory_WithValidId_ReturnsInventory()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Inventory", jsonString);
        }

        [Fact]
        public async Task GetInventory_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateInventory_WithValidInventory_CreatesInventory()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newInventory = new Inventory
            {
                ProductId = 1,
                Quantity = 100,
                Location = "Warehouse A"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/inventory", newInventory);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Warehouse A", jsonString);
        }

        [Fact]
        public async Task UpdateInventory_WithValidInventory_UpdatesInventory()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatedInventory = new Inventory
            {
                ProductId = 1,
                Quantity = 150,
                Location = "Warehouse B"
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/inventory/1", updatedInventory);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Warehouse B", jsonString);
        }

        [Fact]
        public async Task DeleteInventory_WithValidId_DeletesInventory()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/inventory/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}