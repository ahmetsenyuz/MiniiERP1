using Microsoft.AspNetCore.Mvc;
using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound($"Purchase order with ID {id} not found.");
            }

            return Ok(purchaseOrder);
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrdersBySupplier(int supplierId)
        {
            var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersBySupplierIdAsync(supplierId);
            return Ok(purchaseOrders);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPurchaseOrder = await _purchaseOrderService.CreatePurchaseOrderAsync(purchaseOrder);
            if (createdPurchaseOrder == null)
            {
                return BadRequest("Invalid supplier or product reference.");
            }

            return CreatedAtAction(nameof(GetPurchaseOrder), new { id = createdPurchaseOrder.Id }, createdPurchaseOrder);
        }

        [HttpPost("{id}/confirm")]
        public async Task<ActionResult<PurchaseOrder>> ConfirmPurchaseOrder(int id)
        {
            var purchaseOrder = await _purchaseOrderService.ConfirmPurchaseOrderAsync(id);
            if (purchaseOrder == null)
            {
                return BadRequest("Cannot confirm purchase order. It might not exist or is not in pending status.");
            }

            return Ok(purchaseOrder);
        }

        [HttpPost("{id}/cancel")]
        public async Task<ActionResult<bool>> CancelPurchaseOrder(int id)
        {
            var result = await _purchaseOrderService.CancelPurchaseOrderAsync(id);
            if (!result)
            {
                return BadRequest("Cannot cancel purchase order. It might not exist or is not in pending status.");
            }

            return Ok(true);
        }
    }
}