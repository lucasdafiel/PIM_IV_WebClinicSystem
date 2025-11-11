using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Expõe o repositório de pacientes.
        IPacienteRepository Pacientes { get; }
        // Expõe o repositório de profissionais.
        IProfissionalRepository Profissionais { get; }
        // Expõe o repositório de usuários.
        IUsuarioRepository Usuarios { get; }
        // Expõe o repositório de consultas.
        IConsultaRepository Consultas { get; }
        // Expõe o repositório de prontuários.
        IProntuarioRepository Prontuarios { get; }

        // O método que de fato salvará todas as alterações no banco de dados.
        Task<int> CompleteAsync();
        Task<int> CommitAsync(); // Adicionado para compatibilidade
    }
}
