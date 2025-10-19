using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Profissionais.DTOs
{
    // DTO para receber os dados na atualização de um profissional.
    public record UpdateProfissionalDto(
        string NomeCompleto,
        string Especialidade,
        string? Telefone);
}
