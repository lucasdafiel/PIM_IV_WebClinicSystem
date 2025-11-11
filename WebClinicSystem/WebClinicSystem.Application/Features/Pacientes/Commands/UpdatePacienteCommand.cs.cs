using MediatR;
using System;
using WebClinicSystem.Application.Features.Pacientes.DTOs; // Importar DTOs

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    public class UpdatePacienteCommand : IRequest<PacienteDto>
    {
        public int IdPaciente { get; set; } // O ID (da rota)
        public UpdatePacienteDto Dto { get; set; } // O DTO (do corpo)
    }
}