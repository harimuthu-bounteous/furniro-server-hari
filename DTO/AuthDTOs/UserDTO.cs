namespace furniro_server_hari.DTO.AuthDTOs
{
    public class UserDTO
    {
        public string? Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = string.Empty;
    }
}