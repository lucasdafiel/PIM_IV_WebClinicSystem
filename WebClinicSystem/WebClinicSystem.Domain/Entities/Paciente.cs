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
        public int PacienteId { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string TelefoneContato { get; private set; }
        // Novas propriedades que estavam no seu script.
        public string? Email { get; private set; } // O '?' indica que o campo pode ser nulo.
        public DateTime DataCadastro { get; private set; }
    }
}
