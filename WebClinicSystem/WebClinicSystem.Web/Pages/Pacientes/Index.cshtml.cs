using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinicSystem.Application.Features.Pacientes.DTOs; // Use o DTO
using WebClinicSystem.Web.Services; // Importe o serviço
using System; // Adicionado para Exception
using System.Linq; // Adicionado para .ToList()
using System.Collections.Generic; // Adicionado para List<T>
using System.Threading.Tasks; // Adicionado para Task
using Microsoft.AspNetCore.Authorization; // Adicionado para [Authorize]

namespace WebClinicSystem.Web.Pages.Pacientes
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class IndexModel : PageModel
    {
        private readonly IPacienteApiService _pacienteApiService;

        public IndexModel(IPacienteApiService pacienteApiService)
        {
            _pacienteApiService = pacienteApiService;
        }

        public IList<PacienteDto> Pacientes { get; set; } = new List<PacienteDto>();

        // O OnGetAsync está correto
        public async Task OnGetAsync()
        {
            try
            {
                var pacientesResult = await _pacienteApiService.GetAllAsync();
                if (pacientesResult != null)
                {
                    Pacientes = pacientesResult.ToList();
                }
            }
            catch (Exception ex)
            {
                // Adiciona uma mensagem de erro para o usuário via TempData
                TempData["ErrorMessage"] = $"Não foi possível carregar os pacientes: {ex.Message}";
            }
        }

        // --- INÍCIO DA MODIFICAÇÃO (TAREFA 1) ---

        /// <summary>
        /// Handler para exclusão de paciente.
        /// </summary>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                // 1. Chama o serviço da API para deletar
                await _pacienteApiService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Paciente excluído com sucesso!";
            }
            catch (Exception ex)
            {
                // Se a API falhar (ex: paciente não encontrado ou erro interno)
                TempData["ErrorMessage"] = $"Erro ao excluir o paciente: {ex.Message}";
            }

            // 2. Recarrega a página (o que vai disparar o OnGetAsync novamente)
            return RedirectToPage();
        }

        // --- FIM DA MODIFICAÇÃO ---
    }
}