using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Profissionais.Commands
{
    public record DeleteProfissionalCommand(int Id) : IRequest;
}
