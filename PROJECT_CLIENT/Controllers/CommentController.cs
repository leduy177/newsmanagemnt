using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    [Authorize(Roles = "MODERATOR")]
    public class CommentController : Controller
    {
        private readonly BaseService _baseService = new();

       
        public async Task<IActionResult> Index(string? searchTitle, int page = 1, int pageSize = 5)
        {
            var allArticles = await _baseService.GetData<IEnumerable<CommentDTO>>("Comment");

            if (!string.IsNullOrWhiteSpace(searchTitle))
                allArticles = allArticles.Where(a => a.Content.Contains(searchTitle, StringComparison.OrdinalIgnoreCase));

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
        public async Task<IActionResult> Create(CommentDTO dto)
        {
            dto.PostedAt = DateTime.Now;
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;

            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("Comment", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", message ?? "Không thể tạo bình luận.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _baseService.GetData<CommentDTO>($"Comment/{id}");
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CommentUpdateDTO dto)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;

            var (isSuccess, message) = await _baseService.PutAndReadMessageAsync($"Comment/{id}", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", message ?? "Không thể cập nhật bình luận.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;

            var (isSuccess, message) = await _baseService.DeleteAndReadMessageAsync($"Comment/{id}", token!);

            TempData[isSuccess ? "Message" : "Error"] = (isSuccess ? "✔ " : "✘ ") + message;
            return RedirectToAction("Index");
        }
    }
}
