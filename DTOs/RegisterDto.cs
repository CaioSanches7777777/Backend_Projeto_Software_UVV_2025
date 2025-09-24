using System.ComponentModel.DataAnnotations;

namespace Backend_login.DTOs
{
    public class RegisterDto
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
