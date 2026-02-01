using MiniiERP1.Models;

namespace MiniiERP1.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(LoginRequest loginRequest);
        Task<User?> RegisterAsync(User user);
        Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
    }
}