using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminClientController : Controller
    {
        private readonly BaseService _baseService;

        public AdminClientController()
        {
            _baseService = new BaseService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("admin/assign-role", dto, token);

            TempData["Message"] = isSuccess ? $"✅ {message}" : $"❌ Lỗi: {message}";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DisableAccount(DisableAccountDto dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("admin/disable-account", dto, token);

            TempData["Message"] = isSuccess ? $"✅ {message}" : $"❌ Lỗi: {message}";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EnableAccount(DisableAccountDto dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("admin/enable-account", dto, token);

            TempData["Message"] = isSuccess ? $"✅ {message}" : $"❌ Lỗi: {message}";
            return RedirectToAction("Index");
        }
    }
}
