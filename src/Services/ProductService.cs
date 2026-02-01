using MiniiERP1.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniiERP1.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            if (!await IsSkuUniqueAsync(product.SKU))
                return null;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return null;

            if (product.SKU != existingProduct.SKU && !await IsSkuUniqueAsync(product.SKU, id))
                return null;

            existingProduct.Name = product.Name;
            existingProduct.SKU = product.SKU;
            existingProduct.SellingPrice = product.SellingPrice;
            existingProduct.CriticalStockLevel = product.CriticalStockLevel;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            // Check if product has associated orders (simplified check)
            // In a real application, you would check for actual order associations
            var hasOrders = await _context.OrderItems.AnyAsync(oi => oi.ProductId == id);
            if (hasOrders)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null)
        {
            return !await _context.Products
                .AnyAsync(p => p.SKU == sku && p.Id != excludeId);
        }
    }
}