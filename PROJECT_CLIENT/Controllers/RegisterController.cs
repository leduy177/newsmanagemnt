using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    public class RegisterController : Controller
    {
        private readonly BaseService _baseService;

        public RegisterController()
        {
            _baseService = new BaseService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _baseService.RegisterAsync(dto);

            if (result.IsSuccess)
            {
                TempData["Success"] = "✅ Đăng ký thành công. Vui lòng đăng nhập.";
                return RedirectToAction("Login", "Account");
            }

            TempData["Error"] = $"❌ Đăng ký thất bại: {result.Error}";
            return RedirectToAction("Index");
        }
    }
}
