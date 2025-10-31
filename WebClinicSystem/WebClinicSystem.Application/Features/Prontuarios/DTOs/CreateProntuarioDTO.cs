using System;

namespace WebClinicSystem.Application.Features.Prontuarios.DTOs
{
    // DTO para criar um prontuário
    public class CreateProntuarioDTO
    {
        public int IdConsulta { get; set; }
        public string Descricao { get; set; }
    }
}
