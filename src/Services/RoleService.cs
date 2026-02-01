using MiniiERP1.Models;

namespace MiniiERP1.Services
{
    public class RoleService : IRoleService
    {
        private static readonly Dictionary<string, UserRole[]> _modulePermissions = new()
        {
            // Administrator can access all modules
            ["admin"] = new[] { UserRole.Administrator },
            
            // Operation User can only access these modules
            ["products"] = new[] { UserRole.Administrator, UserRole.OperationUser },
            ["suppliers"] = new[] { UserRole.Administrator, UserRole.OperationUser },
            ["purchaseorders"] = new[] { UserRole.Administrator, UserRole.OperationUser }
        };

        public bool HasAccess(UserRole userRole, string module)
        {
            if (_modulePermissions.TryGetValue(module.ToLower(), out var allowedRoles))
            {
                return allowedRoles.Contains(userRole);
            }

            // Default to no access if module is not defined
            return false;
        }
    }
}