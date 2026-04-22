using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _auth.Login(request.Username, request.Password);

            if (token == null) return Unauthorized();

            return Ok(new { token });
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest request)
        {
            var user = await _auth.Register(request.Username, request.Password);

            if (user == null) return Unauthorized();

            return Ok(new { user });
        }
    }
}
