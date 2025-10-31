using MediatR;
using System;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Query para gerar relatório de consultas por período e profissional
    public class GetRelatorioConsultasQuery : IRequest<DTOs.RelatorioConsultasDTO>
    {
        // Data inicial do filtro
        public DateTime DataInicio { get; set; }
        // Data final do filtro
        public DateTime DataFim { get; set; }
        // Id do profissional (opcional)
        public int? IdProfissional { get; set; }
    }
}
