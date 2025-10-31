using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Handler para gerar relatório de consultas
    public class GetRelatorioConsultasQueryHandler : IRequestHandler<GetRelatorioConsultasQuery, RelatorioConsultasDTO>
    {
        private readonly IConsultaRepository _consultaRepository;

        // Injeta o repositório de consultas
        public GetRelatorioConsultasQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<RelatorioConsultasDTO> Handle(GetRelatorioConsultasQuery request, CancellationToken cancellationToken)
        {
            // Busca as consultas pelo período e profissional (se informado)
            var consultas = await _consultaRepository.GetConsultasPorPeriodoEProfissionalAsync(
                request.DataInicio,
                request.DataFim,
                request.IdProfissional ?? 0 // Se não informado, busca todas
            );

            // Mapeia para DTO
            var consultaDtos = consultas.Select(c => new ConsultaDTO
            {
                Id = c.ConsultaId,
                IdPaciente = c.PacienteId,
                NomePaciente = c.Paciente?.NomeCompleto ?? string.Empty,
                IdProfissional = c.ProfissionalId,
                NomeProfissional = c.Profissional?.NomeCompleto ?? string.Empty,
                DataHoraInicio = c.DataHoraInicio,
                DataHoraFim = c.DataHoraFim,
                Status = c.Status
            }).ToList();

            // Retorna o DTO do relatório
            return new RelatorioConsultasDTO
            {
                Consultas = consultaDtos,
                TotalConsultas = consultaDtos.Count
            };
        }
    }
}
