using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Prontuario
    {
        public int ProntuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataRegistro { get; set; }
        public int ConsultaId { get; set; } // Chave estrangeira para Consulta
    }
}
