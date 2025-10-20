using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }  
        public string SenhaHash { get; set; }
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }
    }
}
