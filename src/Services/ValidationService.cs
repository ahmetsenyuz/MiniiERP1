using MiniiERP1.Models;
using System.Text.RegularExpressions;

namespace MiniiERP1.Services
{
    public class ValidationService
    {
        public static ValidationResult ValidateProduct(Product product)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                result.Errors.Add("Product name is required");
            }

            if (string.IsNullOrWhiteSpace(product.SKU))
            {
                result.Errors.Add("SKU is required");
            }

            if (product.SellingPrice < 0)
            {
                result.Errors.Add("Selling price cannot be negative");
            }

            if (product.CriticalStockLevel < 0)
            {
                result.Errors.Add("Critical stock level cannot be negative");
            }

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        public static ValidationResult ValidateSupplier(Supplier supplier)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(supplier.CompanyName))
            {
                result.Errors.Add("Company name is required");
            }

            if (string.IsNullOrWhiteSpace(supplier.ContactPerson))
            {
                result.Errors.Add("Contact person is required");
            }

            if (string.IsNullOrWhiteSpace(supplier.Phone))
            {
                result.Errors.Add("Phone number is required");
            }

            if (string.IsNullOrWhiteSpace(supplier.Email))
            {
                result.Errors.Add("Email is required");
            }

            if (!IsValidEmail(supplier.Email))
            {
                result.Errors.Add("Invalid email format");
            }

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        public static ValidationResult ValidatePurchaseOrder(PurchaseOrder order)
        {
            var result = new ValidationResult();

            if (order.Items == null || order.Items.Count == 0)
            {
                result.Errors.Add("Purchase order must have at least one item");
            }

            foreach (var item in order.Items)
            {
                if (item.Quantity <= 0)
                {
                    result.Errors.Add("Item quantity must be greater than zero");
                }

                if (item.UnitPrice < 0)
                {
                    result.Errors.Add("Item unit price cannot be negative");
                }
            }

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}