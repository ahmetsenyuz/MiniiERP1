using MiniiERP1.Models;

namespace MiniiERP1.Services
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id);
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId);
        Task<PurchaseOrder?> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder?> ConfirmPurchaseOrderAsync(int id);
        Task<bool> CancelPurchaseOrderAsync(int id);
    }
}