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

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            // ToListAsync() executa a consulta no banco e retorna todos os registros da tabela Pacientes.
            return await _context.Pacientes.ToListAsync();
        }
        // Implementação do método para buscar um paciente pelo ID.
        public async Task<Paciente> GetByIdAsync(int id)
        {
            // FindAsync é um método otimizado do EF Core para buscar um item pela sua chave primária.
            return await _context.Pacientes.FindAsync(id);
        }

        // Implementação do método para deletar um paciente.
        public void Delete(Paciente paciente)
        {
            // O método Remove do EF Core marca a entidade para ser excluída no banco.
            _context.Pacientes.Remove(paciente);
        }

        // Implementação do método para buscar um paciente pelo CPF.
        public async Task<Paciente> GetByCpfAsync(string cpf)
        {
            // FirstOrDefaultAsync retorna o primeiro paciente que encontrar com o CPF
            // ou null se não encontrar nenhum.
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
        }
    }
}
