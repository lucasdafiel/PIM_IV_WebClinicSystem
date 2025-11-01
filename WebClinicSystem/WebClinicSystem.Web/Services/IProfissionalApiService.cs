// Importe os DTOs do seu projeto Application
using WebClinicSystem.Application.Features.Profissionais.DTOs;

namespace WebClinicSystem.Web.Services
{
    public interface IProfissionalApiService
    {
        Task<IEnumerable<ProfissionalDto>> GetAllAsync();
        Task<ProfissionalDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProfissionalDto dto);
        Task UpdateAsync(int id, UpdateProfissionalDto dto);
        Task DeleteAsync(int id);
    }
}