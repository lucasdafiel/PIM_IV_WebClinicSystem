using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string NomeCompleto { get; set; }
        public string Especialidade { get; set; }
        // Novas propriedades do seu script.
        public string RegistroConselho { get; set; }
        public string? Telefone { get; set; }
        public int? UsuarioId { get; set; } // Chave estrangeira para a futura entidade Usuario.
    }
}
