using System.ComponentModel.DataAnnotations;

namespace WebClinicSystem.Application.Features.Auth.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public int PerfilId { get; set; }
    }
}
