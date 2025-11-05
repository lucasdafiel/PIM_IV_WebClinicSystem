using System;

namespace WebClinicSystem.Application.Features.Usuarios.DTOs
{
    // DTO de resposta, usado para exibir usuários
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; } // Incluímos o nome do perfil para facilitar
    }
}