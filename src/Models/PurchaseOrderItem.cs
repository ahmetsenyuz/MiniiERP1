using System.ComponentModel.DataAnnotations;

namespace MiniiERP1.Models
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal LineTotal => Quantity * UnitPrice;
    }
}