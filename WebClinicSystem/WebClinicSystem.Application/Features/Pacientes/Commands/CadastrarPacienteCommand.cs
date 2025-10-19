using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    using MediatR;

    // Um "record" é uma forma moderna e concisa de criar uma classe para transportar dados.
    // IRequest<Guid> significa que este comando, quando executado, retornará um Guid (o ID do novo paciente).
    public record CadastrarPacienteCommand(
        string NomeCompleto,
        string Cpf,
        DateTime DataNascimento,
        string TelefoneContato) : IRequest<Guid>;
}
