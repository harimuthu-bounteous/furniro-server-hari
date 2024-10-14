using AutoMapper;
using furniro_server_hari.DTO.AuthDTOs;
using furniro_server_hari.Interfaces;
using furniro_server_hari.Models;
using furniro_server_hari.Services;
using Microsoft.AspNetCore.Mvc;

namespace furniro_server_hari.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public AuthController(IUserRepository userRepository, IMapper mapper, AuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            var result = await _authService.RegisterUserAsync(registerDto.Email, registerDto.Password);
            if (!result)
            {
                return BadRequest("Registration failed");
            }
            await _userRepository.CreateUserAsync(user);
            return Ok("User created successfully!");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var token = await _authService.LoginUserAsync(loginDto.Email, loginDto.Password);
            if (token is null)
            {
                return Unauthorized("Invalid login credentials");
            }
            return Ok(new { token });
        }
    }

}