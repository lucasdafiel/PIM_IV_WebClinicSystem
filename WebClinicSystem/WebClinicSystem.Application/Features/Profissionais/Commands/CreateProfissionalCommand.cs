using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Profissionais.Commands
{
    public record CreateProfissionalCommand(
    string NomeCompleto,
    string Especialidade,
    string RegistroConselho,
    string? Telefone) : IRequest<int>;
}
