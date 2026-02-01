using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniiERP1.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(PurchaseOrderStatus))]
        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Pending;

        [Required]
        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }

    public enum PurchaseOrderStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}