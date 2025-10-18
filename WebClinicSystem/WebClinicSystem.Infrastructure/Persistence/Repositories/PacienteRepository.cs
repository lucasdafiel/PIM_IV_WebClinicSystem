using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebClinicSystem.Infrastructure.Persistence.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        // Dependência do nosso DbContext para acessar o banco de dados.
        private readonly WebClinicDbContext _context;

        public PacienteRepository(WebClinicDbContext context)
        {
            _context = context;
        }

        // Implementação do método para adicionar um paciente.
        public async Task AddAsync(Paciente paciente)
        {
            // O método AddAsync do EF Core marca a entidade para ser inserida no banco.
            await _context.Pacientes.AddAsync(paciente);
        }

        // Implementação do método para buscar um paciente pelo ID.
        public async Task<Paciente> GetByIdAsync(Guid id)
        {
            // FindAsync é um método otimizado do EF Core para buscar um item pela sua chave primária.
            return await _context.Pacientes.FindAsync(id);
        }
    }
}
