using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Service;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });

            if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
                return StatusCode(423, new { message = "Tài khoản đang bị khoá" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });

            var token = await _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                token,
                username = user.UserName
            });
        }

    }
}
