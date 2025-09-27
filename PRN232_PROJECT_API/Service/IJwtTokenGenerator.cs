using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Service
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
