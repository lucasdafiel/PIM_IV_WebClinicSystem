using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Profissionais.DTOs;

namespace WebClinicSystem.Application.Features.Profissionais.Queries
{
    public record GetAllProfissionaisQuery() : IRequest<IEnumerable<ProfissionalDto>>;
}
