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

        // O método que de fato salvará todas as alterações no banco de dados.
        Task<int> CompleteAsync();
    }
}
