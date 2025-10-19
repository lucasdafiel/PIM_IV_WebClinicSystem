using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Profissionais.DTOs;

namespace WebClinicSystem.Application.Features.Profissionais.Queries
{
    // Query que carrega o ID do profissional a ser buscado.
    // Espera um ProfissionalDto (ou nulo) como resposta.
    public record GetProfissionalByIdQuery(int Id) : IRequest<ProfissionalDto?>;
}
