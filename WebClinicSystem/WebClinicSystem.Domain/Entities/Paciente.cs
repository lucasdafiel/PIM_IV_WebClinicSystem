using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Entities
{
    public class Paciente
    {
        // Propriedades da entidade, com setters privados para proteger a integridade dos dados.
        public Guid Id { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string TelefoneContato { get; private set; }

        // Construtor para garantir que um paciente seja criado com os dados essenciais.
        public Paciente(string nomeCompleto, string cpf, DateTime dataNascimento, string telefoneContato)
        {
            Id = Guid.NewGuid(); // Gera um identificador único
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            TelefoneContato = telefoneContato;
        }
    }
}
