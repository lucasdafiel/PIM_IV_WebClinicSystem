using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Pacientes.DTOs
{
    // DTO para receber os dados na atualização de um paciente.
    public record UpdatePacienteDto(
        string NomeCompleto,
        DateTime DataNascimento,
        string TelefoneContato,
        string? Email
    );
}
