using LoginDashboardApi.Models;
using LoginDashboardApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Required for IConfiguration

namespace LoginDashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration; // For accessing appsettings for hardcoded user

        // Hardcoded user for simplicity
        private static readonly User HardcodedUser = new User { Username = "testuser", Password = "password123" };

        public AuthController(TokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            // Validate credentials (replace with database lookup in a real app)
            if (loginRequest.Username == HardcodedUser.Username && loginRequest.Password == HardcodedUser.Password)
            {
                var token = _tokenService.GenerateToken(HardcodedUser);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
