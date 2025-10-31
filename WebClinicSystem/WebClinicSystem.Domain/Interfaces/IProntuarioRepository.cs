using System;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    // Interface para operações de persistência da entidade Prontuario
    public interface IProntuarioRepository
    {
        // Adiciona um prontuário
        Task AddAsync(Prontuario prontuario);

        // Busca um prontuário pelo Id da Consulta
        Task<Prontuario> GetByConsultaIdAsync(int consultaId);
    }
}
