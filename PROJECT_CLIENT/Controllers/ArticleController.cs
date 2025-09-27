using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CLIENT.DTO;
using PROJECT_CLIENT.Service;

namespace PROJECT_CLIENT.Controllers
{
    public class ArticleController : Controller
    {
        private readonly BaseService _baseService = new();

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5, string? searchTitle = null, string? filterTime = null)
        {
            var allArticles = await _baseService.GetData<IEnumerable<ArticleDto>>("Articles/by-user");

            if (!string.IsNullOrWhiteSpace(searchTitle))
                allArticles = allArticles.Where(a => a.Title?.Contains(searchTitle, StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrWhiteSpace(filterTime))
            {
                var now = DateTime.UtcNow;
                allArticles = filterTime switch
                {
                    "24h" => allArticles.Where(a => (now - a.CreatedAt).TotalHours <= 24),
                    "7d" => allArticles.Where(a => (now - a.CreatedAt).TotalDays <= 7),
                    "30d" => allArticles.Where(a => (now - a.CreatedAt).TotalDays <= 30),
                    _ => allArticles
                };
            }

            var totalItems = allArticles.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedArticles = allArticles.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTitle = searchTitle;
            ViewBag.FilterTime = filterTime;

            return View(pagedArticles);
        }





        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _baseService.GetData<IEnumerable<CategoryDTO>>("Category");
            ViewBag.Tags = await _baseService.GetData<IEnumerable<TagDTO>>("Tag");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var dto = new ArticleCreateDto
            {
                Title = form["Title"],
                Content = form["Content"],
                Source = form["Source"],
                CategoryId = int.Parse(form["CategoryId"]),
                ImageUrl = form["ImageUrl"],
                Status = 0,
                TagIds = form["TagIdsInput"]
                            .ToString()
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.TryParse(s.Trim(), out var tagId) ? tagId : -1)
                            .Where(tagId => tagId > 0)
                            .ToList()
            };

            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.PostAndReadMessageAsync("Articles", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            TempData["Error"] = $"✘ {message}";
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var article = await _baseService.GetData<ArticleDto>($"Articles/{id}");
            var updateDto = new ArticleUpdateDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Source = article.Source,
                CategoryId = 0,
                ImageUrl = article.ImageUrl ?? "",
                TagIds = new List<int>()
            };

            ViewBag.ArticleInfo = article;
            ViewBag.Categories = await _baseService.GetData<IEnumerable<CategoryDTO>>("Category");
            ViewBag.Tags = await _baseService.GetData<IEnumerable<TagDTO>>("Tag");

            return View(updateDto);
        }

        [HttpPost]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            var dto = new ArticleUpdateDto
            {
                Id = id,
                Title = form["Title"],
                Content = form["Content"],
                Source = form["Source"],
                CategoryId = int.Parse(form["CategoryId"]),
                ImageUrl = form["ImageUrl"],
                Status = int.Parse(form["Status"]),
                TagIds = form["TagIdsInput"]
                            .ToString()
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.TryParse(s.Trim(), out var tagId) ? tagId : -1)
                            .Where(tagId => tagId > 0)
                            .ToList()
            };

            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.PutAndReadMessageAsync($"Articles/{id}", dto, token!);

            if (isSuccess)
            {
                TempData["Message"] = $"✔ {message}";
                return RedirectToAction("Index");
            }

            TempData["Error"] = $"✘ {message}";
            return RedirectToAction("Edit", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var article = await _baseService.GetData<ArticleDto>($"Articles/{id}");
            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "MODERATOR,AUTHOR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.DeleteAndReadMessageAsync($"Articles/{id}", token!);

            if (isSuccess)
                TempData["Message"] = $"✔ {message}";
            else
                TempData["Error"] = $"✘ {message}";

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> Moderate(int page = 1, int pageSize = 5, string? searchTitle = null)
        {
            var articles = await _baseService.GetData<IEnumerable<ArticleDto>>("Articles");

            var filtered = articles
                .Where(a => (a.Status == 0 || a.Status == 2) &&
                            (string.IsNullOrWhiteSpace(searchTitle) || a.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            int totalItems = filtered.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paged = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTitle = searchTitle;

            return View(paged);
        }


        [HttpPost]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> Moderate(int id, bool approve)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
            var (isSuccess, message) = await _baseService.PutAndReadMessageAsync<object>($"Articles/moderate/{id}?approve={approve}", null, token!);

            TempData["Message"] = isSuccess
                ? (approve ? $"✔ {message}" : $"✘ {message}")
                : $"⚠ Có lỗi xảy ra: {message}";

            var articles = await _baseService.GetData<IEnumerable<ArticleDto>>("Articles");
            var pendingArticles = articles.Where(a => a.Status == 0 || a.Status == 2);
            return View(pendingArticles);
        }
    }
}
