using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WebClinicSystem.Application.Features.Pacientes.DTOs;

namespace WebClinicSystem.Application.Features.Pacientes.Queries
{
    // A query carrega o ID e espera um PacienteDto ou nulo como resposta.
    public record GetPacienteByIdQuery(int Id) : IRequest<PacienteDto?>;
}
