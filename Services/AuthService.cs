// using furniro_server_hari.Models;
// using Supabase.Gotrue;
using Supabase;

namespace furniro_server_hari.Services
{
    public class AuthService
    {
        private readonly Client _client;

        public AuthService(IConfiguration configuration)
        {
            string supabaseUrl = configuration["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url", "Supabase URL is missing from configuration.");
            string supabaseKey = configuration["Supabase:ApiKey"] ?? throw new ArgumentNullException("Supabase:ApiKey", "Supabase ApiKey is missing from configuration.");
            _client = new Client(supabaseUrl, supabaseKey);
            _client.InitializeAsync().Wait();
        }

        public Client GetClient()
        {
            return _client;
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            var response = await GetClient().Auth.SignUp(email, password);
            return response != null;
        }

        public async Task<string?> LoginUserAsync(string email, string password)
        {
            var session = await GetClient().Auth.SignInWithPassword(email, password);
            return session?.AccessToken; // Return the AccessToken for authentication
        }


        // public async Task<User?> GetCurrentUserAsync(string token)
        // {
        //     var user = await GetClient().Auth.GetUser(token);
        //     return user;
        // }

        public async Task LogoutAsync()
        {
            await GetClient().Auth.SignOut();
        }

    }
}