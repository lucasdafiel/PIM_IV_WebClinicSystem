using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Infrastructure.Persistence.Repositories
{
    // Implementação do repositório de consultas
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly WebClinicDbContext _context;

        public ConsultaRepository(WebClinicDbContext context)
        {
            _context = context;
        }

        // Adiciona uma nova consulta
        public async Task AddAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
        }

        // Busca uma consulta pelo Id (int)
        public async Task<Consulta> GetByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Profissional)
                .FirstOrDefaultAsync(c => c.ConsultaId == id);
        }

        // Busca todas as consultas
        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Profissional)
                .ToListAsync();
        }

        // Busca consultas por período e profissional
        public async Task<IEnumerable<Consulta>> GetConsultasPorPeriodoEProfissionalAsync(DateTime inicio, DateTime fim, int idProfissional)
        {
            return await _context.Consultas
                .Where(c => c.ProfissionalId == idProfissional && c.DataHoraInicio >= inicio && c.DataHoraFim <= fim)
                .Include(c => c.Paciente) // Garante a inclusão do Paciente, mesmo que opcional
                .Include(c => c.Profissional) // Garante a inclusão do Profissional, mesmo que opcional
                .ToListAsync();
        }
    }
}