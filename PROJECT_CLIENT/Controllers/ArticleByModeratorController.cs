using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    public class ArticleByModeratorController : Controller
    {
        private readonly BaseService _baseService = new();

        [Authorize(Roles = "MODERATOR")]
       
        public async Task<IActionResult> Index(string? searchTitle, int page = 1, int pageSize = 5)
        {
            var allArticles = await _baseService.GetData<IEnumerable<ArticleDto>>("Articles");

            if (!string.IsNullOrWhiteSpace(searchTitle))
                allArticles = allArticles.Where(a => a.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase));

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
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _baseService.GetData<ArticleDto>($"Articles/{id}");
            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.DeleteAndReadMessageAsync($"Articles/{id}", token!);

            TempData[isSuccess ? "Message" : "Error"] = (isSuccess ? "✔ " : "✘ ") + message;
            return RedirectToAction("Index");
        }
    }
}
