using Microsoft.AspNetCore.Mvc;
using MiniiERP1.Models;
using MiniiERP1.Services;

namespace MiniiERP1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new { error = "Username and password are required" });
            }

            var user = await _userService.AuthenticateAsync(loginRequest);
            if (user == null)
            {
                return Unauthorized(new { error = "Invalid credentials" });
            }

            var token = _userService.GenerateJwtToken(user);
            return Ok(new TokenResponse
            {
                Token = token,
                ExpiresIn = 24 * 60 * 60 // 24 hours in seconds
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registeredUser = await _userService.RegisterAsync(user);
            if (registeredUser == null)
            {
                return BadRequest(new { error = "Username or email already exists" });
            }

            return CreatedAtAction(nameof(Register), new { id = registeredUser.Id }, registeredUser);
        }
    }
}