using System;

namespace WebClinicSystem.Application.Features.Prontuarios.DTOs
{
    // DTO para exibição de prontuário com nome do profissional
    public class ProntuarioDTO
    {
        public int ProntuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string NomeProfissional { get; set; }
    }
}
