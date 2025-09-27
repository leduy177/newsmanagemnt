using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository;
using System.Security.Claims;

namespace PRN232_PROJECT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserArticleController : ControllerBase
    {
        private readonly IUserArticleRepository _repository;
        private readonly IMapper _mapper;

        public UserArticleController(IUserArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserArticleDTO>>(list));
        }

        [HttpGet("{userId}/{articleId}")]
        public async Task<IActionResult> Get(string userId, int articleId)
        {
            var record = await _repository.GetByIdAsync(userId, articleId);
            if (record == null) return NotFound();
            return Ok(_mapper.Map<UserArticleDTO>(record));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserArticleCreateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(dto.UserId, dto.ArticleId);
            if (existing != null)
            {
                return Conflict(new { success = false, message = "Người dùng đã tồn tại trong bài báo." });
            }

            try
            {
                var entity = _mapper.Map<UserArticle>(dto);
                await _repository.AddAsync(entity);

                return CreatedAtAction(nameof(Get),
                    new { userId = dto.UserId, articleId = dto.ArticleId },
                    new
                    {
                        success = true,
                        message = "Tạo người dùng vào bài viết thành công.",
                        data = _mapper.Map<UserArticleDTO>(entity)
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi khi tạo người dùng vào bài viết.",
                    error = ex.Message
                });
            }
        }


        [HttpPut("{userId}/{articleId}")]
        public async Task<IActionResult> Update(string userId, int articleId, UserArticleDTO dto)
        {
            var existing = await _repository.GetByIdAsync(userId, articleId);
            if (existing == null) return NotFound();
             try
            {
                _mapper.Map(dto, existing);
                await _repository.UpdateAsync(existing);

                return Ok(new { success = true, message = "Cập nhật chuyên mục thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi cập nhật.", error = ex.Message });
            }
            
        }

        [HttpDelete("{userId}/{articleId}")]
        public async Task<IActionResult> Delete(string userId, int articleId)
        {
            await _repository.DeleteAsync(userId, articleId);
            return NoContent();
        }
        [Authorize(Roles = "AUTHOR")]
        [HttpGet("by-user")]
        public async Task<IActionResult> GetByCurrentUser()
        {
            // 🔐 Lấy userId từ JWT claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Không xác định được người dùng." });

            var articles = await _repository.GetAllByUserIdAsync(userId);
            var authorArticles = articles.Where(ua => ua.RoleInArticle == "AUTHOR");

            return Ok(_mapper.Map<IEnumerable<UserArticleDTO>>(authorArticles));
        }
        [Authorize(Roles = "AUTHOR")]
        [HttpPost("add-to-article/{articleId}")]
        public async Task<IActionResult> AddUserToArticle(int articleId, [FromBody] UserArticleCreateDTO dto)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized(new { message = "Bạn chưa đăng nhập." });

            var authorships = await _repository.GetAllByUserIdAsync(currentUserId);
            bool isAuthorOfThisArticle = authorships.Any(x => x.ArticleId == articleId && x.RoleInArticle == "AUTHOR");

            if (!isAuthorOfThisArticle)
                return Forbid("Bạn không có quyền thêm người vào bài viết này.");

            dto.ArticleId = articleId;
            var exists = await _repository.GetByIdAsync(dto.UserId, articleId);
            if (exists != null)
                return Conflict(new { message = "Người dùng này đã có trong bài viết." });

            var entity = _mapper.Map<UserArticle>(dto);
            await _repository.AddAsync(entity);

            return Ok(new { message = "Đã thêm thành công." });
        }
        [HttpGet("by-article/{articleId}")]
        public async Task<IActionResult> GetByArticleId(int articleId)
        {
            var list = await _repository.GetAllAsync();
            var filtered = list.Where(x => x.ArticleId == articleId);
            return Ok(_mapper.Map<IEnumerable<UserArticleDTO>>(filtered));
        }
        // ✅ XOÁ người khỏi bài viết
        [Authorize(Roles = "AUTHOR")]
        [HttpDelete("remove-from-article/{articleId}/{userId}")]
        public async Task<IActionResult> RemoveUserFromArticle(int articleId, string userId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized(new { message = "Bạn chưa đăng nhập." });

            var authorships = await _repository.GetAllByUserIdAsync(currentUserId);
            bool isAuthorOfThisArticle = authorships.Any(x => x.ArticleId == articleId && x.RoleInArticle == "AUTHOR");

            if (!isAuthorOfThisArticle)
                return Forbid("Bạn không có quyền xoá người khỏi bài viết này.");

            var existing = await _repository.GetByIdAsync(userId, articleId);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy người dùng trong bài viết." });

            await _repository.DeleteAsync(userId, articleId);
            return Ok(new { message = "Đã xoá thành công." });
        }
    }
}
