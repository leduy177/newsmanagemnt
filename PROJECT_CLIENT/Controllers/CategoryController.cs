using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    [Authorize(Roles = "AUTHOR")]
    public class CategoryController : Controller
    {
        private readonly BaseService _baseService = new();

        
        public async Task<IActionResult> Index(string? searchTitle, int page = 1, int pageSize = 5)
        {
            var allArticles = await _baseService.GetData<IEnumerable<CategoryDTO>>("Category");

            if (!string.IsNullOrWhiteSpace(searchTitle))
                allArticles = allArticles.Where(a => a.Name.Contains(searchTitle, StringComparison.OrdinalIgnoreCase));

            var paged = allArticles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allArticles.Count() / (double)pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTitle = searchTitle;

            return View(paged);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("Category", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", message ?? "Không thể tạo category.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _baseService.GetData<CategoryDTO>($"Category/{id}");
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryDTO dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.PutAndReadMessageAsync($"Category/{id}", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", message ?? "Không thể cập nhật category.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.DeleteAndReadMessageAsync($"Category/{id}", token!);

            TempData[isSuccess ? "Message" : "Error"] = (isSuccess ? "✔ " : "✘ ") + message;
            return RedirectToAction("Index");
        }
    }
}
