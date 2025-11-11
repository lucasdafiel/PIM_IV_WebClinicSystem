using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebClinicSystem.Application.Features.Consultas.DTOs; // Use o DTO
using WebClinicSystem.Web.Services; // Importe o serviço

namespace WebClinicSystem.Web.Pages.Agenda
{
    [Authorize(Roles = "Administrador,Recepcionista")] // Protege a página
    public class IndexModel : PageModel
    {
        private readonly IConsultaApiService _consultaApiService;
        // Vamos precisar dos outros serviços para preencher os <select> no formulário
        private readonly IPacienteApiService _pacienteApiService;
        private readonly IProfissionalApiService _profissionalApiService;

        public IndexModel(IConsultaApiService consultaApiService, IPacienteApiService pacienteApiService, IProfissionalApiService profissionalApiService)
        {
            _consultaApiService = consultaApiService;
            _pacienteApiService = pacienteApiService;
            _profissionalApiService = profissionalApiService;
        }

        // --- Propriedades para os Filtros ---
        [BindProperty(SupportsGet = true)] // SupportsGet permite que sejam lidos da URL
        public int? FiltroIdProfissional { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? FiltroDataInicio { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? FiltroDataFim { get; set; }

        // --- Propriedades para a Página ---
        public IList<ConsultaDTO> Consultas { get; set; } = new List<ConsultaDTO>();

        // Listas para preencher os Dropdowns (selects) de filtro e agendamento
        public SelectList PacientesOptions { get; set; }
        public SelectList ProfissionaisOptions { get; set; }

        // --- Propriedade para o Formulário de Novo Agendamento ---
        [BindProperty]
        public CreateConsultaDTO NovaConsulta { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Carrega os dados para os filtros
                await LoadDropdowns();

                // Busca as consultas na API usando os filtros
                var consultasResult = await _consultaApiService.GetAllAsync(FiltroIdProfissional, FiltroDataInicio, FiltroDataFim);
                if (consultasResult != null)
                {
                    Consultas = consultasResult.ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar consultas: {ex.Message}");
                // TempData["ErrorMessage"] = "Não foi possível carregar a agenda.";
            }
        }

        // Método para carregar os dropdowns
        private async Task LoadDropdowns()
        {
            var pacientes = await _pacienteApiService.GetAllAsync();
            var profissionais = await _profissionalApiService.GetAllAsync();

            PacientesOptions = new SelectList(pacientes, "IdPaciente", "NomeCompleto");
            ProfissionaisOptions = new SelectList(profissionais, "IdProfissional", "NomeCompleto");
        }

        // Handler para o formulário de Agendamento (POST)
        public async Task<IActionResult> OnPostAgendarAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(); // Recarrega os dropdowns se o modelo for inválido
                return Page();
            }

            try
            {
                await _consultaApiService.AgendarAsync(NovaConsulta);
                // TempData["SuccessMessage"] = "Consulta agendada com sucesso!";
            }
            catch (HttpRequestException ex)
            {
                // Aqui você pode tratar erros da API (ex: RN-09 - Conflito de agenda)
                Console.WriteLine($"Erro ao agendar consulta: {ex.Message}");
                // TempData["ErrorMessage"] = "Erro ao agendar: " + ex.Message;
            }

            return RedirectToPage(); // Recarrega a página
        }

        // Handler para o botão Cancelar (POST)
        // Usamos um handler nomeado para diferenciar do agendamento
        public async Task<IActionResult> OnPostCancelarAsync(int idConsulta)
        {
            try
            {
                await _consultaApiService.CancelarAsync(idConsulta);
                // TempData["SuccessMessage"] = "Consulta cancelada.";
            }
            catch (HttpRequestException ex)
            {
                // Trata erros (ex: RN-11 - Consulta já concluída)
                Console.WriteLine($"Erro ao cancelar consulta: {ex.Message}");
                // TempData["ErrorMessage"] = "Erro ao cancelar: " + ex.Message;
            }

            return RedirectToPage(); // Recarrega a página
        }
    }
}