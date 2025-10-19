using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WebClinicSystem.Application.Features.Pacientes.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Pacientes.Queries
{
    public class GetAllPacientesQueryHandler : IRequestHandler<GetAllPacientesQuery, IEnumerable<PacienteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPacientesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PacienteDto>> Handle(GetAllPacientesQuery request, CancellationToken cancellationToken)
        {
            var pacientes = await _unitOfWork.Pacientes.GetAllAsync();

            // Mapeia a lista de entidades para uma lista de DTOs.
            return pacientes.Select(p => new PacienteDto(
                p.PacienteId,
                p.NomeCompleto,
                p.Cpf,
                p.DataNascimento,
                p.TelefoneContato,
                p.Email));
        }
    }
}
