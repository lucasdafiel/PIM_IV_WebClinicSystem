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
    public class GetPacienteByIdQueryHandler : IRequestHandler<GetPacienteByIdQuery, PacienteDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPacienteByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PacienteDto?> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
        {
            // Usa o repositório para buscar o paciente no banco.
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(request.Id);

            // Se o paciente não for encontrado, retorna nulo.
            if (paciente is null)
                return null;

            // Mapeia a entidade para o DTO de resposta.
            return new PacienteDto(
                paciente.PacienteId,
                paciente.NomeCompleto,
                paciente.Cpf,
                paciente.DataNascimento,
                paciente.TelefoneContato,
                paciente.Email);
        }
    }
}
