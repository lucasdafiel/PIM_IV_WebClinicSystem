using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Infrastructure.Persistence.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly WebClinicDbContext _context;

        public ProfissionalRepository(WebClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profissional profissional)
        {
            await _context.Profissionais.AddAsync(profissional);
        }

        public async Task<Profissional> GetByIdAsync(int id)
        {
            return await _context.Profissionais.FindAsync(id);
        }

        public async Task<IEnumerable<Profissional>> GetAllAsync()
        {
            return await _context.Profissionais.ToListAsync();
        }

        public void Update(Profissional profissional)
        {
            _context.Profissionais.Update(profissional);
        }

        public void Delete(Profissional profissional)
        {
            _context.Profissionais.Remove(profissional);
        }
    }
}
