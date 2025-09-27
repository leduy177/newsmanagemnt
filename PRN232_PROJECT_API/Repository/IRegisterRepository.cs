using PRN232_PROJECT_API.DTO;

namespace PRN232_PROJECT_API.Repository
{
    public interface IRegisterRepository
    {
        Task<(bool IsSuccess, string? UserId, string? Error)> RegisterAsync(RegisterDto dto);
    }
}
