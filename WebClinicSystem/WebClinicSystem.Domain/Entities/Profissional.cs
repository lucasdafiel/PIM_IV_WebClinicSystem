using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Profissional
    {
        public Guid Id { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Especialidade { get; private set; }

        public Profissional(string nomeCompleto, string especialidade)
        {
            Id = Guid.NewGuid();
            NomeCompleto = nomeCompleto;
            Especialidade = especialidade;
        }
    }
}
