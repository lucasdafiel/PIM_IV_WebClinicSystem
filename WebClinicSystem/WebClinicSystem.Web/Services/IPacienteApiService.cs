using System.Collections.Generic;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Pacientes.DTOs;

namespace WebClinicSystem.Web.Services
{
    public interface IPacienteApiService
    {
        // Métodos para listar todos e buscar por ID
        Task<IEnumerable<PacienteDto>> GetAllAsync();
        Task<PacienteDto> GetByIdAsync(int id);

        // Métodos para criar, atualizar e deletar
        Task CreateAsync(PacienteDto dto);
        Task UpdateAsync(int id, UpdatePacienteDto dto);
        Task DeleteAsync(int id);
    }
}