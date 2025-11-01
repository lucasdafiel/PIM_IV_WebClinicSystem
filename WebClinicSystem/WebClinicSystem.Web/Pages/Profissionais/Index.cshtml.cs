using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinicSystem.Application.Features.Profissionais.DTOs; // Use o DTO
using WebClinicSystem.Web.Services; // Importe o serviço
using Microsoft.AspNetCore.Authorization; // Importe o Authorize

namespace WebClinicSystem.Web.Pages.Profissionais
{
    [Authorize] // Protege a página
    public class IndexModel : PageModel
    {
        // Injete o novo serviço
        private readonly IProfissionalApiService _profissionalApiService;

        public IndexModel(IProfissionalApiService profissionalApiService)
        {
            _profissionalApiService = profissionalApiService;
        }

        // A lista agora é de ProfissionalDto
        public IList<ProfissionalDto> Profissionais { get; set; } = new List<ProfissionalDto>();

        // O OnGetAsync agora chama a API
        public async Task OnGetAsync()
        {
            try
            {
                var profissionaisResult = await _profissionalApiService.GetAllAsync();
                if (profissionaisResult != null)
                {
                    Profissionais = profissionaisResult.ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                // Tratar erro (opcional, mas recomendado)
                Console.WriteLine($"Erro ao buscar profissionais: {ex.Message}");
                // TempData["ErrorMessage"] = "Não foi possível carregar os profissionais.";
            }
        }
    }
}