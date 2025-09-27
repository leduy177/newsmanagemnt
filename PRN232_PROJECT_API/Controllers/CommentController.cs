using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository;
using System.Security.Claims;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null)
                return NotFound(new { message = "Không tìm thấy bình luận." });

            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Bạn cần đăng nhập để bình luận." });

            var comment = new Comment
            {
                ArticleId = dto.ArticleId,
                Content = dto.Content,
                PostedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _repository.AddAsync(comment);
            var createdDto = _mapper.Map<CommentDTO>(comment);

            return CreatedAtAction(nameof(Get), new { id = comment.Id }, new
            {
                message = "Bình luận đã được tạo thành công.",
                data = createdDto
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, CommentUpdateDTO dto)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null)
                return NotFound(new { message = "Không tìm thấy bình luận để cập nhật." });

            _mapper.Map(dto, comment);
            await _repository.UpdateAsync(comment);

            return Ok(new { message = "Bình luận đã được cập nhật thành công." });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null)
                return NotFound(new { message = "Không tìm thấy bình luận để xoá." });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (comment.UserId != currentUserId && userRole != "MODERATOR")
                return StatusCode(403, new { message = "Bạn không có quyền xoá bình luận này." });

            await _repository.DeleteAsync(id);
            return Ok(new { message = "Bình luận đã được xoá thành công." });
        }
    }
}
