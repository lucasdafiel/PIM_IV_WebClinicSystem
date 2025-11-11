using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Pacientes.DTOs;
using WebClinicSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebClinicSystem.Web.Pages.Pacientes
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class CreateModel : PageModel
    {
        private readonly IPacienteApiService _pacienteApiService;

        public CreateModel(IPacienteApiService pacienteApiService)
        {
            _pacienteApiService = pacienteApiService;
        }

        [BindProperty]
        public CreatePacienteDto Paciente { get; set; } = new CreatePacienteDto();

        public void OnGet()
        {
            // Apenas exibe o formulário vazio
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _pacienteApiService.CreateAsync(Paciente);

                TempData["SuccessMessage"] = "Paciente cadastrado com sucesso!";

                return RedirectToPage("/Pacientes/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao criar paciente: {ex.Message}");
                return Page();
            }
        }
    }
}