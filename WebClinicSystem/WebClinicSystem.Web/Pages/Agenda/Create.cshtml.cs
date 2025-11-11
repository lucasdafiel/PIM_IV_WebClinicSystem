using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Agenda
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class CreateModel : PageModel
    {
        private readonly IConsultaApiService _consultaApiService;
        private readonly IPacienteApiService _pacienteApiService;
        private readonly IProfissionalApiService _profissionalApiService;

        public CreateModel(IConsultaApiService consultaApiService, IPacienteApiService pacienteApiService, IProfissionalApiService profissionalApiService)
        {
            _consultaApiService = consultaApiService;
            _pacienteApiService = pacienteApiService;
            _profissionalApiService = profissionalApiService;
        }

        // SelectLists para os dropdowns
        public SelectList PacientesOptions { get; set; }
        public SelectList ProfissionaisOptions { get; set; }

        // Modelo do formulário
        [BindProperty]
        public AgendamentoFormModel NovaConsulta { get; set; } = new AgendamentoFormModel();

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            var dto = new CreateConsultaDTO
            {
                IdPaciente = NovaConsulta.IdPaciente,
                IdProfissional = NovaConsulta.IdProfissional,
                DataHoraInicio = NovaConsulta.DataHoraInicio
            };

            await _consultaApiService.AgendarAsync(dto);

            TempData["SuccessMessage"] = "Consulta agendada com sucesso!";
            return RedirectToPage("/Agenda/Index");
        }

        private async Task LoadDropdownsAsync()
        {
            var pacientes = await _pacienteApiService.GetAllAsync();
            PacientesOptions = new SelectList(pacientes, "Id", "NomeCompleto");

            var profissionais = await _profissionalApiService.GetAllAsync();
            ProfissionaisOptions = new SelectList(profissionais, "Id", "NomeCompleto");
        }
    }

    public class AgendamentoFormModel
    {
        [Required(ErrorMessage = "Selecione um paciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Selecione um profissional")]
        public int IdProfissional { get; set; }

        [Required(ErrorMessage = "Informe data e hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraInicio { get; set; }
    }
}
