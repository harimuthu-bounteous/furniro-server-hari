using furniro_server_hari.Interfaces;
using furniro_server_hari.Models;
using Supabase;

namespace furniro_server_hari.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = [];
        private readonly Client _client;

        public UserRepository(Client client)
        {
            _client = client;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            // var user = _users.FirstOrDefault(u => u.UserId == id);
            // return Task.FromResult(user);

            var response = await _client.From<User>().Where(x => x.UserId == id).Single();
            return response;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            // var user = _users.FirstOrDefault(u => u.Email == email);
            // return Task.FromResult(user);

            var response = await _client.From<User>().Where(x => x.Email == email).Single();
            return response;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            // _users.Add(user);
            // return Task.FromResult(user);

            var response = await _client.From<User>().Insert(user);
            return response.Model;
        }
    }
}