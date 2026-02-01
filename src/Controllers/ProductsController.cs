using Microsoft.AspNetCore.Mvc;
using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search term is required");
            }

            var products = await _productService.SearchProductsByNameAsync(name);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _productService.CreateProductAsync(product);
            if (createdProduct == null)
                return BadRequest("SKU must be unique");

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            if (updatedProduct == null)
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                    return NotFound();
                
                return BadRequest("SKU must be unique");
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                    return NotFound();
                
                return BadRequest("Cannot delete product with associated orders");
            }

            return NoContent();
        }
    }
}