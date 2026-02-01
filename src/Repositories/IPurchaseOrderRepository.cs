using MiniiERP1.Models;

namespace MiniiERP1.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder?> GetByIdAsync(int id);
        Task<IEnumerable<PurchaseOrder>> GetBySupplierIdAsync(int supplierId);
        Task<PurchaseOrder?> CreateAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder?> UpdateAsync(PurchaseOrder purchaseOrder);
    }
}