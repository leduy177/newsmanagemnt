using System.ComponentModel.DataAnnotations;

namespace PRN232_PROJECT_API.DTO
{
    public class RegisterDto
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        
    }
}
