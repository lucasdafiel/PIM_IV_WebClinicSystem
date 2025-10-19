using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    // Comando para deletar um paciente. Ele recebe o ID e não retorna nada (IRequest).
    public record DeletePacienteCommand(int PacienteId) : IRequest;
}
