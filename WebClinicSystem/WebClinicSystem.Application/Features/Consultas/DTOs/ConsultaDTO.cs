using System;

namespace WebClinicSystem.Application.Features.Consultas.DTOs
{
    // DTO para exibir informações da consulta
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public string NomePaciente { get; set; }
        public int IdProfissional { get; set; }
        public string NomeProfissional { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; }
    }
}