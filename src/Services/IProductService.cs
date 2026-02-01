using MiniiERP1.Models;

namespace MiniiERP1.Services
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);
        Task<Product?> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null);
    }
}