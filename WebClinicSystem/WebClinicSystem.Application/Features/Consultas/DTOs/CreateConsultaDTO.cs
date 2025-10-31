using System;

namespace WebClinicSystem.Application.Features.Consultas.DTOs
{
    // DTO para receber dados do agendamento
    public class CreateConsultaDTO
    {
        public int IdPaciente { get; set; }
        public int IdProfissional { get; set; }
        public DateTime DataHoraInicio { get; set; }
    }
}