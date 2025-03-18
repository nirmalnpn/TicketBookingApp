using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using TicketBookingApp.Services;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Check if username exists
                var existingUser = await _userRepository.GetUserByUsername(model.Username);
                if (existingUser != null)
                    return BadRequest(new { error = "Username already exists." });

                // Hash password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // Create new user
                var user = new User
                {
                    FullName = model.FullName,
                    Username = model.Username,
                    Email = model.Email,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    NationalIDNumber = model.NationalIDNumber,
                    PasswordHash = hashedPassword,
                    Role = "User"
                };

                await _userRepository.RegisterUser(user);
                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred during registration.", details = ex.Message });
            }
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userRepository.GetUserByUsername(model.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    return Unauthorized(new { error = "Invalid username or password." });

                // Generate JWT token with expiration time
                var token = _jwtService.GenerateToken(user);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred during login.", details = ex.Message });
            }
        }
    }

    // Models with validation
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, MinLength(4)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, RegularExpression(@"^\d{8,20}$", ErrorMessage = "Invalid National ID format.")]
        public string NationalIDNumber { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
