using Microsoft.AspNetCore.Identity;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, string? UserId, string? Error)> RegisterAsync(RegisterDto dto)
        {
            // Kiểm tra trùng Email
            var existingEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existingEmail != null)
            {
                return (false, null, "Email đã được sử dụng.");
            }

            // Kiểm tra trùng UserName
            var existingUser = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                return (false, null, "Tên người dùng đã tồn tại.");
            }

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "VIEWER");
                return (true, user.Id, null);
            }

            var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Đăng ký thất bại vì lý do không xác định.";
            return (false, null, errorMessage);
        }

    }
}
