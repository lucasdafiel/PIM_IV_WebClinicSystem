using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Pacientes.DTOs;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    public class UpdatePacienteCommandHandler : IRequestHandler<UpdatePacienteCommand, PacienteDto>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePacienteCommandHandler(IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork)
        {
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PacienteDto> Handle(UpdatePacienteCommand request, CancellationToken cancellationToken)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(request.IdPaciente);

            if (paciente == null)
            {
                return null;
            }

            // Aplica as mudanças do DTO
            paciente.NomeCompleto = request.Dto.NomeCompleto;
            paciente.TelefoneContato = request.Dto.TelefoneContato;

            // Esta linha (CS1061) será corrigida na próxima etapa
            _pacienteRepository.Update(paciente);

            // Esta linha (CS1061) será corrigida na próxima etapa
            await _unitOfWork.CompleteAsync();

            // Retorna o DTO atualizado
            return new PacienteDto(
                paciente.PacienteId,
                paciente.NomeCompleto,
                paciente.Cpf,
                paciente.DataNascimento,
                paciente.TelefoneContato,
                paciente.Email
            );
        }
    }
}