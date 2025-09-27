using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository.IRepository;
using System.Security.Claims;

namespace PRN232_PROJECT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticlesController(IArticlesRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _articleRepository.GetAllAsync();
            var articleDtos = _mapper.Map<List<ArticleDto>>(articles);
            return Ok(articleDtos);
        }
        [EnableQuery]
        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedArticles()
        {
            var allArticles = await _articleRepository.GetAllAsync();
            var approved = allArticles.Where(a => a.Status == 1).ToList();
            var result = _mapper.Map<List<ArticleDto>>(approved);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest("ID phải là số nguyên dương.");

            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return NotFound("Không tìm thấy bài viết với ID tương ứng.");

            var articleDto = _mapper.Map<ArticleDto>(article);
            return Ok(articleDto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? category, [FromQuery] string? time)
        {
            var articles = await _articleRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(title))
                articles = articles.Where(a => a.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(category))
                articles = articles.Where(a => a.Category != null && a.Category.Name == category).ToList();

            if (!string.IsNullOrWhiteSpace(time))
            {
                var now = DateTime.UtcNow;
                articles = time switch
                {
                    "24h" => articles.Where(a => (now - a.CreatedAt).TotalHours <= 24).ToList(),
                    "7d" => articles.Where(a => (now - a.CreatedAt).TotalDays <= 7).ToList(),
                    "30d" => articles.Where(a => (now - a.CreatedAt).TotalDays <= 30).ToList(),
                    _ => articles
                };
            }

            var result = _mapper.Map<List<ArticleDto>>(articles);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Create([FromBody] ArticleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không thể xác định người dùng.");

            try
            {
                var article = _mapper.Map<Article>(dto);
                article.CreatedAt = DateTime.UtcNow;
                article.Status = 0;
                article.UserArticles = new List<UserArticle>
                {
                    new UserArticle { UserId = userId, RoleInArticle = "AUTHOR" }
                };
                article.ArticleTags = dto.TagIds?.Select(tagId => new ArticleTag { TagId = tagId }).ToList();

                await _articleRepository.AddAsync(article);

                var createdArticle = await _articleRepository.GetByIdAsync(article.Id);
                if (createdArticle == null)
                    return StatusCode(500, "Tạo bài viết không thành công.");

                return CreatedAtAction(nameof(Get), new { id = article.Id }, _mapper.Map<ArticleDto>(createdArticle));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "AUTHOR, MODERATOR")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleUpdateDto dto)
        {
            if (id < 1)
                return BadRequest("ID không hợp lệ.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _articleRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy bài viết để cập nhật.");

            try
            {
                _mapper.Map(dto, existing);
                existing.ArticleTags.Clear();

                if (dto.TagIds != null)
                {
                    foreach (var tagId in dto.TagIds)
                    {
                        existing.ArticleTags.Add(new ArticleTag { ArticleId = existing.Id, TagId = tagId });
                    }
                }

                await _articleRepository.UpdateAsync(existing);
                return Ok("Cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi cập nhật bài viết: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "AUTHOR, MODERATOR")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("ID không hợp lệ.");

            var existing = await _articleRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy bài viết để xoá.");

            try
            {
                await _articleRepository.DeleteAsync(existing);
                return Ok(new { success = true, message = "Xoá bài viết thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi xoá bài viết: {ex.Message}");
            }
        }

        [HttpGet("by-user")]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> GetArticlesByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không thể xác định người dùng.");

            var allArticles = await _articleRepository.GetAllAsync();
            var authoredArticles = allArticles
                .Where(a => a.UserArticles.Any(ua => ua.UserId == userId && ua.RoleInArticle == "AUTHOR"))
                .ToList();

            var result = _mapper.Map<List<ArticleDto>>(authoredArticles);
            return Ok(result);
        }

        [HttpPut("moderate/{id}")]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> Moderate(int id, [FromQuery] bool approve)
        {
            if (id < 1)
                return BadRequest("ID không hợp lệ.");

            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return NotFound("Bài báo không tồn tại.");

            try
            {
                article.Status = approve ? 1 : 2;
                await _articleRepository.UpdateAsync(article);

                return Ok(new
                {
                    success = true,
                    message = approve ? "Đã duyệt bài báo." : "Đã từ chối bài báo."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi xử lý bài viết: {ex.Message}");
            }
        }
    }
}
