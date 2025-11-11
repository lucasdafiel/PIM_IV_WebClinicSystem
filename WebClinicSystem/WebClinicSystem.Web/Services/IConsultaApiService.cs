// Importe os DTOs do seu projeto Application
using WebClinicSystem.Application.Features.Consultas.DTOs;

namespace WebClinicSystem.Web.Services
{
    public interface IConsultaApiService
    {
        // Método para listar consultas com filtros (RF-05)
        Task<IEnumerable<ConsultaDTO>> GetAllAsync(int? idProfissional, DateTime? dataInicio, DateTime? dataFim);

        // Método para agendar (criar) uma nova consulta
        Task AgendarAsync(CreateConsultaDTO dto);

        // Método para cancelar uma consulta
        Task CancelarAsync(int id);

        // Método para o relatório (RF-08)
        Task<RelatorioConsultasDTO> GetRelatorioAsync(DateTime dataInicio, DateTime dataFim, int? idProfissional);

        // Método para obter uma consulta por ID
        Task<ConsultaDTO> GetByIdAsync(int id);
    }
}