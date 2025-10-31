using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Infrastructure.Persistence.Repositories
{
    // Implementação do repositório de prontuários
    public class ProntuarioRepository : IProntuarioRepository
    {
        private readonly WebClinicDbContext _context;

        public ProntuarioRepository(WebClinicDbContext context)
        {
            _context = context;
        }

        // Adiciona um prontuário
        public async Task AddAsync(Prontuario prontuario)
        {
            await _context.Prontuarios.AddAsync(prontuario);
        }

        // Busca um prontuário pelo Id da Consulta
        public async Task<Prontuario> GetByConsultaIdAsync(int consultaId)
        {
            // Inclui a consulta para poder acessar informações relacionadas (ex: Profissional)
            return await _context.Prontuarios
                .Include(p => p.Consulta)
                    .ThenInclude(c => c.Profissional)
                .FirstOrDefaultAsync(p => p.ConsultaId == consultaId);
        }
    }
}
