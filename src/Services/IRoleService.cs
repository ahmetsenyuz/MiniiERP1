using MiniiERP1.Models;

namespace MiniiERP1.Services
{
    public interface IRoleService
    {
        bool HasAccess(UserRole userRole, string module);
    }
}