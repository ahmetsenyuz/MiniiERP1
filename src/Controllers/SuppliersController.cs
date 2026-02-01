using Microsoft.AspNetCore.Mvc;
using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly LoggingService _loggingService;

        public SuppliersController(ISupplierService supplierService, LoggingService loggingService)
        {
            _supplierService = supplierService;
            _loggingService = loggingService;
        }

        // // <summary>
        // // Gets all suppliers
        // // </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // // <summary>
        // // Gets a supplier by ID
        // // </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound($"Supplier with ID {id} not found.");
            }

            return Ok(supplier);
        }

        // // <summary>
        // // Creates a new supplier
        // // </summary>
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            try
            {
                var validationResult = ValidationService.ValidateSupplier(supplier);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        _loggingService.LogValidationError("CreateSupplier", error);
                    }
                    return BadRequest(new { error = validationResult.Errors.FirstOrDefault() });
                }

                var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
                return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.Id }, createdSupplier);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An internal error occurred while creating the supplier." });
            }
        }

        // // <summary>
        // // Updates an existing supplier
        // // </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> UpdateSupplier(int id, Supplier supplier)
        {
            try
            {
                var validationResult = ValidationService.ValidateSupplier(supplier);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        _loggingService.LogValidationError("UpdateSupplier", error);
                    }
                    return BadRequest(new { error = validationResult.Errors.FirstOrDefault() });
                }

                var updatedSupplier = await _supplierService.UpdateSupplierAsync(id, supplier);
                if (updatedSupplier == null)
                {
                    return NotFound($"Supplier with ID {id} not found.");
                }

                return Ok(updatedSupplier);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An internal error occurred while updating the supplier." });
            }
        }

        // // <summary>
        // // Deletes a supplier
        // // </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            if (!result)
            {
                return NotFound($"Supplier with ID {id} not found.");
            }

            return Ok(new { message = $"Supplier with ID {id} deleted successfully." });
        }
    }
}