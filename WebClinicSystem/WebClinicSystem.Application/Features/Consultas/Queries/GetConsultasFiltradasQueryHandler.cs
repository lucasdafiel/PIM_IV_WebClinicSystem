using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Handler para buscar consultas filtradas por profissional e período (RF-05)
    public class GetConsultasFiltradasQueryHandler : IRequestHandler<GetConsultasFiltradasQuery, IEnumerable<ConsultaDTO>>
    {
        private readonly IConsultaRepository _consultaRepository;

        public GetConsultasFiltradasQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<IEnumerable<ConsultaDTO>> Handle(GetConsultasFiltradasQuery request, CancellationToken cancellationToken)
        {
            // Define período padrão caso não informado (busca todas as datas)
            var dataInicio = request.DataInicio ?? DateTime.MinValue;
            var dataFim = request.DataFim ?? DateTime.MaxValue;

            // Se IdProfissional não informado, passa 0 para buscar todos no repositório (implementação do repositório trata 0 como todos)
            var idProfissional = request.IdProfissional ?? 0;

            var consultas = await _consultaRepository.GetConsultasPorPeriodoEProfissionalAsync(dataInicio, dataFim, idProfissional);

            // Mapeia para DTO
            return consultas.Select(c => new ConsultaDTO
            {
                Id = c.ConsultaId,
                IdPaciente = c.PacienteId,
                NomePaciente = c.Paciente?.NomeCompleto ?? string.Empty,
                IdProfissional = c.ProfissionalId,
                NomeProfissional = c.Profissional?.NomeCompleto ?? string.Empty,
                DataHoraInicio = c.DataHoraInicio,
                DataHoraFim = c.DataHoraFim,
                Status = c.Status ?? string.Empty
            });
        }
    }
}
