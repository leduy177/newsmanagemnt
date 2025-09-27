using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "ID không hợp lệ." });

            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { success = false, message = "Không tìm thấy chuyên mục." });

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Create(CategoryCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { success = false, message = "Tên chuyên mục không được để trống." });

            var existing = await _repository.GetByNameAsync(dto.Name);
            if (existing != null)
                return Conflict(new { success = false, message = "Tên chuyên mục đã tồn tại." });

            try
            {
                var entity = _mapper.Map<Category>(dto);
                await _repository.AddAsync(entity);

                return CreatedAtAction(nameof(Get), new { id = entity.Id },
                    new { success = true, message = "Tạo chuyên mục thành công.", data = _mapper.Map<CategoryDTO>(entity) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi tạo chuyên mục.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Update(int id, CategoryDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(new { success = false, message = "ID không khớp." });

            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { success = false, message = "Không tìm thấy chuyên mục." });

            try
            {
                _mapper.Map(dto, category);
                await _repository.UpdateAsync(category);

                return Ok(new { success = true, message = "Cập nhật chuyên mục thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi cập nhật.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "AUTHOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { success = false, message = "Không tìm thấy chuyên mục cần xoá." });

            try
            {
                await _repository.DeleteAsync(id);
                return Ok(new { success = true, message = $"Đã xoá chuyên mục '{category.Name}'." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Xoá chuyên mục thất bại.", error = ex.Message });
            }
        }
    }
}
