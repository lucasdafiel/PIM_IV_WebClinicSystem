using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebClinicDbContext _context;

        // Instancia os repositórios quando o UnitOfWork é criado.
        public UnitOfWork(WebClinicDbContext context)
        {
            _context = context;
            Pacientes = new PacienteRepository(_context);
        }

        public IPacienteRepository Pacientes { get; private set; }

        // O método 'CompleteAsync' simplesmente chama o SaveChangesAsync do DbContext.
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Método para liberar a conexão com o banco de dados.
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
