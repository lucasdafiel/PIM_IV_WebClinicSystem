using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Pacientes.DTOs
{
    // Este 'record' serve para receber os dados da requisição para criar um paciente.
    public record PacienteDto(
    int PacienteId,
    string NomeCompleto,
    string Cpf,
    DateTime DataNascimento,
    string TelefoneContato,
    string? Email);
}
