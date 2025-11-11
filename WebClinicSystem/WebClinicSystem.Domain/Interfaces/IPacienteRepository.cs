using System.Collections.Generic;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> GetByIdAsync(int id);
        Task<List<Paciente>> GetAllAsync();
        Task<Paciente> GetByCpfAsync(string cpf);
        Task AddAsync(Paciente paciente);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
    }
}