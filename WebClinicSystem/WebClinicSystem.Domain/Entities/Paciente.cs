using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Paciente
    {
        // O ID agora é um int para bater com a coluna IDENTITY do SQL Server.
        public int PacienteId { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TelefoneContato { get; set; }
        public string? Email { get; set; } // O '?' indica que o campo pode ser nulo.
        public DateTime DataCadastro { get; set; }
    }
}
