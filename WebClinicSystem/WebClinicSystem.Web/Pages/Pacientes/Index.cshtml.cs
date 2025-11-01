using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinicSystem.Application.Features.Pacientes.DTOs; // Use o DTO
using WebClinicSystem.Web.Services; // Importe o serviço

namespace WebClinicSystem.Web.Pages.Pacientes
{
    // Adicione a diretiva [Authorize] para proteger a página.
    // O sistema de Cookies que configuramos vai cuidar disso.
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class IndexModel : PageModel
    {
        // 1. Remova qualquer DbContext ou IUnitOfWork que estava aqui

        // 2. Injete o novo serviço da API
        private readonly IPacienteApiService _pacienteApiService;

        public IndexModel(IPacienteApiService pacienteApiService)
        {
            _pacienteApiService = pacienteApiService;
        }

        // 3. A lista agora é de PacienteDto, não da entidade Paciente
        public IList<PacienteDto> Pacientes { get; set; } = new List<PacienteDto>();

        // 4. O OnGetAsync agora chama a API
        public async Task OnGetAsync()
        {
            try
            {
                // Chama o serviço, que chama a API, que chama o Handler...
                var pacientesResult = await _pacienteApiService.GetAllAsync();
                if (pacientesResult != null)
                {
                    Pacientes = pacientesResult.ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                // Se a API falhar (ex: token expirado ou API offline),
                // você pode tratar o erro aqui.
                // Por enquanto, apenas logamos no console (ideal seria um log real)
                Console.WriteLine($"Erro ao buscar pacientes: {ex.Message}");
                // Você pode adicionar uma mensagem de erro para o usuário
                // TempData["ErrorMessage"] = "Não foi possível carregar os pacientes.";
            }
        }
    }
}