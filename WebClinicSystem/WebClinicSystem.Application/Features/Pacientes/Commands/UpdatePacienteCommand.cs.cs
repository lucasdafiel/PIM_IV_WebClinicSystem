using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    // Comando para atualizar um paciente. Ele não retorna nada (IRequest).
    public record UpdatePacienteCommand(
        int PacienteId,
        string NomeCompleto,
        string Cpf,
        DateTime DataNascimento,
        string TelefoneContato,
        string? Email) : IRequest;
}
