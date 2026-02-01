using MiniiERP1.Models;
using MiniiERP1.Repositories;

namespace MiniiERP1.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly LoggingService _loggingService;

        public PurchaseOrderService(
            IPurchaseOrderRepository purchaseOrderRepository,
            IProductRepository productRepository,
            ISupplierRepository supplierRepository,
            LoggingService loggingService)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _loggingService = loggingService;
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
        {
            return await _purchaseOrderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId)
        {
            return await _purchaseOrderRepository.GetBySupplierIdAsync(supplierId);
        }

        public async Task<PurchaseOrder?> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            // Validate supplier exists
            var supplier = await _supplierRepository.GetByIdAsync(purchaseOrder.SupplierId);
            if (supplier == null)
            {
                _loggingService.LogValidationError("CreatePurchaseOrder", "Invalid supplier reference");
                return null;
            }

            // Validate products exist and calculate total amount
            decimal totalAmount = 0;
            foreach (var item in purchaseOrder.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    _loggingService.LogValidationError("CreatePurchaseOrder", "Invalid product reference");
                    return null;
                }

                item.UnitPrice = product.SellingPrice;
                totalAmount += item.LineTotal;
            }

            purchaseOrder.TotalAmount = totalAmount;
            purchaseOrder.OrderDate = DateTime.UtcNow;

            return await _purchaseOrderRepository.CreateAsync(purchaseOrder);
        }

        public async Task<PurchaseOrder?> ConfirmPurchaseOrderAsync(int id)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
            if (purchaseOrder == null || purchaseOrder.Status != PurchaseOrderStatus.Pending)
            {
                _loggingService.LogValidationError("ConfirmPurchaseOrder", "Cannot confirm purchase order. It might not exist or is not in pending status.");
                return null;
            }

            // Update inventory quantities
            foreach (var item in purchaseOrder.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.StockLevel += item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }

            // Update purchase order status
            purchaseOrder.Status = PurchaseOrderStatus.Confirmed;
            return await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
        }

        public async Task<bool> CancelPurchaseOrderAsync(int id)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
            if (purchaseOrder == null || purchaseOrder.Status != PurchaseOrderStatus.Pending)
            {
                _loggingService.LogValidationError("CancelPurchaseOrder", "Cannot cancel purchase order. It might not exist or is not in pending status.");
                return false;
            }

            purchaseOrder.Status = PurchaseOrderStatus.Cancelled;
            var result = await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
            return result != null;
        }
    }
}