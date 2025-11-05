using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Relatorio
{
    // 8. Adiciona a diretiva [Authorize] restrita ao Administrador
    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        // 1. Injeção de Serviços (campos privados)
        private readonly IConsultaApiService _consultaApiService;
        private readonly IProfissionalApiService _profissionalApiService;

        // 1. Injeção de Serviços (Construtor)
        public IndexModel(IConsultaApiService consultaApiService, IProfissionalApiService profissionalApiService)
        {
            _consultaApiService = consultaApiService;
            _profissionalApiService = profissionalApiService;

            // Inicializa as datas padrão para evitar erros de calendário
            DataInicio = DateTime.Today;
            DataFim = DateTime.Today;
        }

        // 2. Propriedades para os filtros (BindProperty)
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FiltroIdProfissional { get; set; }

        // 3. Propriedade pública para o resultado
        public RelatorioConsultasDTO Relatorio { get; set; }

        // 4. Propriedade pública para o dropdown
        public SelectList ProfissionaisOptions { get; set; }

        // 6. Implementação do OnGetAsync
        public async Task OnGetAsync()
        {
            // No carregamento inicial (GET), apenas populamos os filtros
            await LoadDropdowns();
        }

        // 7. Implementação do OnPostAsync
        public async Task OnPostAsync()
        {
            // 7.1. Recarrega os dropdowns (essencial para repopular a lista após o POST)
            await LoadDropdowns();

            // 7.2. Chama o serviço da API para buscar o relatório com os filtros
            Relatorio = await _consultaApiService.GetRelatorioAsync(DataInicio, DataFim, FiltroIdProfissional);
        }

        // 5. Método privado para carregar dropdowns
        private async Task LoadDropdowns()
        {
            // Busca a lista de profissionais
            var profissionais = await _profissionalApiService.GetAllAsync();

            // Cria o SelectList, usando "Id" como valor e "Nome" como texto
            // (Baseado no exemplo de Agenda/Index.cshtml.cs)
            // O quarto parâmetro (FiltroIdProfissional) garante que o valor selecionado seja mantido
            ProfissionaisOptions = new SelectList(profissionais, "Id", "Nome", FiltroIdProfissional);
        }
    }
}