using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WebClinicSystem.Application.Features.Pacientes.DTOs;

namespace WebClinicSystem.Application.Features.Pacientes.Queries
{
    // Esta query não precisa de parâmetros e espera uma lista de PacienteDto como resposta.
    public record GetAllPacientesQuery() : IRequest<IEnumerable<PacienteDto>>;
}
