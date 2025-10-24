using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Infrastructure.Persistence.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            // Define a chave primária.
            builder.HasKey(p => p.PerfilId);

            // Define os dados iniciais (seeding) para a tabela Perfis.
            builder.HasData(
                new Perfil { PerfilId = 1, Nome = "Administrador" },
                new Perfil { PerfilId = 2, Nome = "Profissional de Saúde" },
                new Perfil { PerfilId = 3, Nome = "Recepcionista" }
            );
        }
    }
}
