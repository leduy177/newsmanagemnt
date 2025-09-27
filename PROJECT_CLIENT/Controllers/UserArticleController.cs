using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;
using System.Net;
using System.Security.Claims;

namespace PROJECT_CLIENT.Controllers
{
    public class UserArticleController : Controller
    {
        private readonly BaseService _service = new();

       
        public async Task<IActionResult> Index(string? searchTitle, int page = 1, int pageSize = 5)
        {
            var allArticles = await _service.GetData<IEnumerable<UserArticleDTO>>("UserArticle/by-user");

            if (!string.IsNullOrWhiteSpace(searchTitle))
                allArticles = allArticles.Where(a => a.RoleInArticle.Contains(searchTitle, StringComparison.OrdinalIgnoreCase));

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

        [HttpGet]
        public IActionResult AddToArticle(int articleId)
        {
            var model = new UserArticleCreateDTO { ArticleId = articleId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToArticle(UserArticleCreateDTO dto)
        {
            var token = HttpContext.User.FindFirst("AccessToken")?.Value;

            var (isSuccess, message) = await _service.PostAndReadMessageAsync($"UserArticle/add-to-article/{dto.ArticleId}", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = "✔ " + message;
                return RedirectToAction("Details", new { articleId = dto.ArticleId });
            }

            ViewBag.Error = "✘ " + message;
            return View(dto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserArticleCreateDTO dto)
        {
            var token = HttpContext.User.FindFirst("AccessToken")?.Value;
            var (isSuccess, message) = await _service.PostAndReadMessageAsync("UserArticle", dto, token!);
            if (isSuccess)
            {
                TempData["Message"] = "✔ " + message;
                return RedirectToAction("Index");
            }

            ViewBag.Error = "✘ " + message;
            return View(dto);
        }

        public async Task<ActionResult> Edit(string userId, int articleId)
        {
            var data = await _service.GetData<UserArticleDTO>($"UserArticle/{userId}/{articleId}");
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int articleId)
        {
            try
            {
                var list = await _service.GetData<IEnumerable<UserArticleDTO>>($"UserArticle/by-article/{articleId}");
                ViewBag.ArticleId = articleId;
                if (TempData["Message"] != null) ViewBag.Message = TempData["Message"];
                if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể tải dữ liệu: " + ex.Message;
                return View(Enumerable.Empty<UserArticleDTO>());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserArticleDTO dto)
        {
            var token = HttpContext.User.FindFirst("AccessToken")?.Value;
            var (isSuccess, message) = await _service.PutAndReadMessageAsync($"UserArticle/{dto.UserId}/{dto.ArticleId}", dto, token!);
            if (isSuccess)
            {
                TempData["Message"] = "✔ " + message;
                return RedirectToAction("Index");
            }

            ViewBag.Error = "✘ " + message;
            return View(dto);
        }

        public async Task<IActionResult> RemoveUser(string userId, int articleId)
        {
            var token = HttpContext.User.FindFirst("AccessToken")?.Value;
            var (isSuccess, message) = await _service.DeleteAndReadMessageAsync($"UserArticle/remove-from-article/{articleId}/{userId}", token!);

            TempData[isSuccess ? "Message" : "Error"] = (isSuccess ? "✔ " : "✘ ") + message;
            return RedirectToAction("Details", new { articleId });
        }

        public async Task<IActionResult> EditRole(string userId, int articleId)
        {
            var data = await _service.GetData<UserArticleDTO>($"UserArticle/{userId}/{articleId}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, int articleId, UserArticleDTO dto)
        {
            var token = HttpContext.User.FindFirst("AccessToken")?.Value;
            var (isSuccess, message) = await _service.PutAndReadMessageAsync($"UserArticle/{userId}/{articleId}", dto, token!);
            if (isSuccess)
            {
                TempData["Message"] = "✔ " + message;
                return RedirectToAction("Details", new { articleId });
            }

            ViewBag.Error = "✘ " + message;
            return View(dto);
        }
    }
}
