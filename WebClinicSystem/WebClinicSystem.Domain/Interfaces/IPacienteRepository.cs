using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> GetByIdAsync(int id);
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task AddAsync(Paciente paciente);
        // Outros métodos como Update, Delete, etc., serão adicionados aqui.
    }
}
