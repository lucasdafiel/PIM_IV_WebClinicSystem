using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Perfil
    {
        // Chave primária da tabela Perfis.
        public int PerfilId { get; private set; }

        // Nome do perfil (ex: "Administrador").
        public string Nome { get; private set; }
    }
}
