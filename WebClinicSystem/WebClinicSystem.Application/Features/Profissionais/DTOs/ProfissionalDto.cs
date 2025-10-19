using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Profissionais.DTOs
{
    // DTO para retornar os dados de um profissional.
    public record ProfissionalDto(
        int ProfissionalId,
        string NomeCompleto,
        string Especialidade,
        string RegistroConselho,
        string? Telefone);
}
