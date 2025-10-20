using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public int PacienteId { get; set; } // Chave estrangeira para Paciente
        public int ProfissionalId { get; set; } // Chave estrangeira para Profissional
    }
}
