using Microsoft.AspNetCore.Mvc.Testing;
using MiniiERP1.Models;

namespace MiniiERP1.Tests.Integration
{
    public class OrdersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public OrdersControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetOrder_WithValidId_ReturnsOrder()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orders/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Order", jsonString);
        }

        [Fact]
        public async Task GetOrder_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orders/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_WithValidOrder_CreatesOrder()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newOrder = new Order
            {
                CustomerName = "Integration Test Customer",
                ProductId = 1,
                Quantity = 2,
                Status = "Pending"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/orders", newOrder);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Integration Test Customer", jsonString);
        }

        [Fact]
        public async Task UpdateOrder_WithValidOrder_UpdatesOrder()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatedOrder = new Order
            {
                CustomerName = "Updated Integration Test Customer",
                ProductId = 1,
                Quantity = 3,
                Status = "Shipped"
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/orders/1", updatedOrder);

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Updated Integration Test Customer", jsonString);
        }

        [Fact]
        public async Task DeleteOrder_WithValidId_DeletesOrder()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/orders/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}