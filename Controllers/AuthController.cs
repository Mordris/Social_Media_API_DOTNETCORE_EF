using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Social_Media_API.Data;
using Social_Media_API.Models;
using Social_Media_API.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Social_Media_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the username or email is already taken
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return Conflict(ModelState);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken.");
                return Conflict(ModelState);
            }

            // Create a new user object
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FullName = model.FullName
            };

            // Add the user to the database
            _context.Users.Add(user);
            _context.SaveChanges();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return the token in the response
            return Ok(new { token });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            // Check if the user exists and the password is correct
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");
                return Unauthorized(ModelState);
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return the token in the response
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
