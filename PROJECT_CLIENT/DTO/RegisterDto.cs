using System.ComponentModel.DataAnnotations;

namespace PROJECT_CLIENT.DTO
{
    public class RegisterDto
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
