using CollegeApp.Data;
using CollegeApp.Dtos;
using CollegeApp.Models;
using CollegeApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _context.AppUsers
                .AnyAsync(u => u.UserName == dto.UserName || u.Email == dto.Email);

            if (userExists)
            {
                return BadRequest(new { message = "Username or email already exists." });
            }

            var user = new AppUser
            {
                FullName = dto.FullName,
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var passwordHash = HashPassword(dto.Password);

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.PasswordHash == passwordHash);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                userName = user.UserName,
                role = user.Role
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
