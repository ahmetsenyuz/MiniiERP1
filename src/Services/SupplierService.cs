using MiniiERP1.Models;
using System.Collections.Concurrent;

namespace MiniiERP1.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ConcurrentDictionary<int, Supplier> _suppliers = new();
        private int _nextId = 1;

        public Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return Task.FromResult(_suppliers.Values.AsEnumerable());
        }

        public Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            _suppliers.TryGetValue(id, out var supplier);
            return Task.FromResult(supplier);
        }

        public Task<Supplier?> GetSupplierByNameAsync(string companyName)
        {
            var supplier = _suppliers.Values.FirstOrDefault(s => s.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(supplier);
        }

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            // Validate supplier
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }

            if (string.IsNullOrEmpty(supplier.CompanyName))
            {
                throw new ArgumentException("Company name is required", nameof(supplier.CompanyName));
            }

            if (string.IsNullOrEmpty(supplier.ContactPerson))
            {
                throw new ArgumentException("Contact person is required", nameof(supplier.ContactPerson));
            }

            if (string.IsNullOrEmpty(supplier.Phone))
            {
                throw new ArgumentException("Phone number is required", nameof(supplier.Phone));
            }

            if (string.IsNullOrEmpty(supplier.Email))
            {
                throw new ArgumentException("Email is required", nameof(supplier.Email));
            }

            if (!IsValidEmail(supplier.Email))
            {
                throw new ArgumentException("Invalid email format", nameof(supplier.Email));
            }

            // Check if company name is unique
            if (!await IsCompanyNameUniqueAsync(supplier.CompanyName))
            {
                throw new InvalidOperationException("A supplier with this company name already exists");
            }

            // Assign ID and add to collection
            supplier.Id = _nextId++;
            _suppliers[supplier.Id] = supplier;

            return supplier;
        }

        public async Task<Supplier?> UpdateSupplierAsync(int id, Supplier supplier)
        {
            if (!_suppliers.ContainsKey(id))
            {
                return null;
            }

            // Validate supplier
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }

            if (string.IsNullOrEmpty(supplier.CompanyName))
            {
                throw new ArgumentException("Company name is required", nameof(supplier.CompanyName));
            }

            if (string.IsNullOrEmpty(supplier.ContactPerson))
            {
                throw new ArgumentException("Contact person is required", nameof(supplier.ContactPerson));
            }

            if (string.IsNullOrEmpty(supplier.Phone))
            {
                throw new ArgumentException("Phone number is required", nameof(supplier.Phone));
            }

            if (string.IsNullOrEmpty(supplier.Email))
            {
                throw new ArgumentException("Email is required", nameof(supplier.Email));
            }

            if (!IsValidEmail(supplier.Email))
            {
                throw new ArgumentException("Invalid email format", nameof(supplier.Email));
            }

            // Check if company name is unique (excluding current supplier)
            if (!await IsCompanyNameUniqueAsync(supplier.CompanyName, id))
            {
                throw new InvalidOperationException("A supplier with this company name already exists");
            }

            // Update supplier
            supplier.Id = id;
            _suppliers[id] = supplier;

            return supplier;
        }

        public Task<bool> DeleteSupplierAsync(int id)
        {
            // In a real implementation, we would check for associated purchase orders here
            // For now, we'll assume suppliers can always be deleted
            return Task.FromResult(_suppliers.TryRemove(id, out _));
        }

        public Task<bool> IsCompanyNameUniqueAsync(string companyName, int? excludeId = null)
        {
            var isUnique = !_suppliers.Values.Any(s => 
                s.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase) && 
                s.Id != excludeId);
            
            return Task.FromResult(isUnique);
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