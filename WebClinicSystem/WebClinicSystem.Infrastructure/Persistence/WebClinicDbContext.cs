using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Infrastructure.Persistence
{
    public class WebClinicDbContext : DbContext
    {
        // O construtor recebe as opções de configuração do EF Core.
        public WebClinicDbContext(DbContextOptions<WebClinicDbContext> options) : base(options)
        {
        }

        // Cada DbSet<T> representa uma tabela no banco de dados.
        // Mapeamos a entidade 'Paciente' para uma tabela chamada 'Pacientes'.
        public DbSet<Paciente> Pacientes { get; set; }

        // Mapeamos a entidade 'Profissional' para uma tabela chamada 'Profissionais'.
        public DbSet<Profissional> Profissionais { get; set; }

        // Poderíamos adicionar configurações mais detalhadas aqui no futuro.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas as configurações de mapeamento de entidades que possamos criar.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebClinicDbContext).Assembly);
        }
    }
}
