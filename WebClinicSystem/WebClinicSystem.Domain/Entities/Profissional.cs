using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Profissional
    {
        public int ProfissionalId { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Especialidade { get; private set; }
        // Novas propriedades do seu script.
        public string RegistroConselho { get; private set; }
        public string? Telefone { get; private set; }
        public int? UsuarioId { get; private set; } // Chave estrangeira para a futura entidade Usuario.
    }
}
