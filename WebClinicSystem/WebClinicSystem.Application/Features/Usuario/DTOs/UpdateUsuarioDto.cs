using System.ComponentModel.DataAnnotations;

namespace WebClinicSystem.Application.Features.Usuarios.DTOs
{
    public class UpdateUsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]

        // --- Correção (Erro CS1739) ---
        // Substitui [Length(min: 3, max: 100...)] por [StringLength(100, MinimumLength = 3...)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório")]
        public int PerfilId { get; set; }
    }
}