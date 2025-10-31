using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Consulta
    {
        // Chave primária
        public int ConsultaId { get; set; }

        // Data/hora de início e fim da consulta
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        // Status da consulta (Ex: Agendada, Cancelada, Concluída)
        public string Status { get; set; } = string.Empty;

        // Chaves estrangeiras
        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }

        // Propriedades de navegação para facilitar consultas com Include
        public Paciente? Paciente { get; set; }
        public Profissional? Profissional { get; set; }
    }
}
