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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly WebClinicDbContext _context;

        public UsuarioRepository(WebClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        // Implementação do método para buscar um usuário pelo email.
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            // FirstOrDefaultAsync busca o primeiro usuário que corresponde ao email fornecido.
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
