using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PROJECT_CLIENT.Controllers
{
    public class AccountController : Controller
    {
        private readonly BaseService _baseService;

        public AccountController()
        {
            _baseService = new BaseService();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var loginResult = await _baseService.LoginAsync(dto);

            if (string.IsNullOrEmpty(loginResult.Token))
            {
                ViewBag.Error = loginResult.Message ?? "Đăng nhập thất bại.";
                return View();
            }

            var token = loginResult.Token;
            HttpContext.Session.SetString("JWTToken", token);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value ?? "";
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid")?.Value ?? "";

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, dto.UserName),
        new Claim("AccessToken", token),
        new Claim(ClaimTypes.Role, role),
        new Claim(ClaimTypes.NameIdentifier, userId)
    };

            HttpContext.Session.SetString("UserId", userId);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return role switch
            {
                "ADMIN" => RedirectToAction("Index", "AdminClient"),
                "AUTHOR" => RedirectToAction("Index", "ManageByAuthor"),
                "MODERATOR" => RedirectToAction("Moderate", "Article"),
                _ => RedirectToAction("Index", "Home"),
            };
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string? token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            var response = await _baseService.GetWithToken<UserProfileDto>("UserProfile/me", token);
            if (response == null)
            {
                TempData["Error"] = "Không thể tải thông tin người dùng.";
                return RedirectToAction("Login");
            }

            return View(response);
        }
    }
}