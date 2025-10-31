using System.Collections.Generic;

namespace WebClinicSystem.Application.Features.Consultas.DTOs
{
    // DTO para o relatório de consultas
    public class RelatorioConsultasDTO
    {
        // Lista de consultas do período
        public IEnumerable<ConsultaDTO> Consultas { get; set; }
        // Total de consultas encontradas
        public int TotalConsultas { get; set; }
    }
}
