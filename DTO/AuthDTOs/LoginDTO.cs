using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace furniro_server_hari.DTO.AuthDTOs
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}