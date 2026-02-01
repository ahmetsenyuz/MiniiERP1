using System.ComponentModel.DataAnnotations;

namespace MiniiERP1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "SKU must be less than 50 characters")]
        public string SKU { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling Price must be greater than zero")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Critical Stock Level must be zero or greater")]
        public int CriticalStockLevel { get; set; }
    }
}