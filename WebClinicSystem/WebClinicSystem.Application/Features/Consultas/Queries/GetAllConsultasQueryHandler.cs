using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Handler para buscar todas as consultas
    public class GetAllConsultasQueryHandler : IRequestHandler<GetAllConsultasQuery, IEnumerable<ConsultaDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllConsultasQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ConsultaDTO>> Handle(GetAllConsultasQuery request, CancellationToken cancellationToken)
        {
            var consultas = await _unitOfWork.Consultas.GetAllAsync();

            // Mapeia para DTO incluindo nomes do paciente e profissional
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