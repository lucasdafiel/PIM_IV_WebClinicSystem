using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

// --- Correção (Erro CS0246) ---
// Adiciona a diretiva using para ConsultaDTO
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Prontuario
{
    [Authorize(Roles = "Administrador,Profissional de Saúde")]
    public class IndexModel : PageModel
    {
        private readonly IProntuarioApiService _prontuarioApiService;
        private readonly IConsultaApiService _consultaApiService;

        public IndexModel(IProntuarioApiService prontuarioApiService, IConsultaApiService consultaApiService)
        {
            _prontuarioApiService = prontuarioApiService;
            _consultaApiService = consultaApiService;
        }

        [BindProperty(SupportsGet = true)]
        public int IdConsulta { get; set; }

        [BindProperty]
        public CreateProntuarioDTO ProntuarioCreate { get; set; }

        public ProntuarioDTO ProntuarioView { get; set; }

        public ConsultaDTO Consulta { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // --- Correção (Erro CS7036) ---
            // Adiciona os parâmetros (null, null, null)
            var todasConsultas = await _consultaApiService.GetAllAsync(null, null, null);
            Consulta = todasConsultas?.FirstOrDefault(c => c.Id == IdConsulta);

            if (Consulta == null)
            {
                TempData["ErrorMessage"] = "A consulta solicitada não foi encontrada.";
                return RedirectToPage("/Agenda/Index");
            }

            ProntuarioView = await _prontuarioApiService.GetByConsultaIdAsync(IdConsulta);

            if (ProntuarioView == null)
            {
                ProntuarioCreate = new CreateProntuarioDTO
                {
                    IdConsulta = IdConsulta
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // --- Correção (Erro CS7036) ---
                // Adiciona os parâmetros (null, null, null)
                var todasConsultas = await _consultaApiService.GetAllAsync(null, null, null);
                Consulta = todasConsultas?.FirstOrDefault(c => c.Id == IdConsulta);

                return Page();
            }

            try
            {
                await _prontuarioApiService.CreateAsync(ProntuarioCreate);
                TempData["SuccessMessage"] = "Prontuário salvo com sucesso!";
                return RedirectToPage("/Agenda/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao salvar o prontuário: {ex.Message}");

                // --- Correção (Erro CS7036) ---
                // Adiciona os parâmetros (null, null, null)
                var todasConsultas = await _consultaApiService.GetAllAsync(null, null, null);
                Consulta = todasConsultas?.FirstOrDefault(c => c.Id == IdConsulta);

                return Page();
            }
        }
    }
}