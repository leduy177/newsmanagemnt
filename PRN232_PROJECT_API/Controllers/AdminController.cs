using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMIN")]

    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return NotFound($"User '{dto.UserName}' Không tìm thấy, vui lòng nhập lại.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, dto.Role);

            return Ok($"User '{user.Email}' đã được cấp role: '{dto.Role}'.");
        }

        [HttpPost("disable-account")]
        public async Task<IActionResult> DisableAccount([FromBody] DisableAccountDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return NotFound($"User '{dto.UserName}' Không tìm thấy, vui lòng nhập lại.");

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;
            await _userManager.UpdateAsync(user);

            return Ok($"User '{user.Email}' is Disable!.");
        }

        [HttpPost("enable-account")]
        public async Task<IActionResult> EnableAccount([FromBody] DisableAccountDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return NotFound($"User '{dto.UserName}' Không tìm thấy, vui lòng nhập lại.");

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return Ok($"User '{user.Email}' is Enable!");
        }
    }
}
