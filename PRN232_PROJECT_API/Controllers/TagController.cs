using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "AUTHOR, MODERATOR")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TagDTO>>(tags));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null) return NotFound();
            return Ok(_mapper.Map<TagDTO>(tag));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagCreateDTO dto)
        {
            // Kiểm tra tên tag trống
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Tên Tag không được để trống."
                });
            }

            // Kiểm tra trùng tên
            var existing = await _repository.GetByNameAsync(dto.Name);
            if (existing != null)
            {
                return Conflict(new
                {
                    success = false,
                    message = "Tên Tag đã tồn tại."
                });
            }

            // Lưu mới
            var entity = _mapper.Map<Tag>(dto);
            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, new
            {
                success = true,
                message = "Tag đã được tạo thành công.",
                data = _mapper.Map<TagDTO>(entity)
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TagDTO dto)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null)
                return NotFound( "Không tìm thấy Tag để cập nhật.");
            try
            {
                _mapper.Map(dto, tag);
                await _repository.UpdateAsync(tag);

                return Ok(new { success = true, message = "Cập nhật chuyên mục thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi cập nhật.", error = ex.Message });
            }
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok("Tag đã được xoá thành công." );
        }
    }
}
