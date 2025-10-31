using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    // Interface para operações de persistência da entidade Consulta
    public interface IConsultaRepository
    {
        // Adiciona uma nova consulta
        Task AddAsync(Consulta consulta);

        // Busca uma consulta pelo Id (int)
        Task<Consulta> GetByIdAsync(int id);

        // Busca todas as consultas
        Task<IEnumerable<Consulta>> GetAllAsync();

        // Busca consultas por período e profissional (filtra por DataHora)
        Task<IEnumerable<Consulta>> GetConsultasPorPeriodoEProfissionalAsync(DateTime inicio, DateTime fim, int idProfissional);
    }
}