using furniro_server_hari.Models;

namespace furniro_server_hari.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> CreateUserAsync(User user);
    }
}