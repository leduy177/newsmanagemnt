using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Repository;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterRepository _registerRepo;

        public RegisterController(IRegisterRepository registerRepo)
        {
            _registerRepo = registerRepo;
        }

        [HttpPost("register-account")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ.");

            var result = await _registerRepo.RegisterAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Error ?? "Đăng ký thất bại."
                });
            }

            return Ok(new
            {
                success = true,
                userId = result.UserId,
                message = "Đăng ký thành công."
            });
        }
    }
}
