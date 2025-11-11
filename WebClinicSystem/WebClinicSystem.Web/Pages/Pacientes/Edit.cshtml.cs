using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Pacientes.DTOs; // Importa DTOs
using WebClinicSystem.Web.Services; // Importa Serviço
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebClinicSystem.Web.Pages.Pacientes
{
    [Authorize(Roles = "Administrador,Recepcionista")] // Protege a página de edição
    public class EditModel : PageModel
    {
        private readonly IPacienteApiService _pacienteApiService;

        public EditModel(IPacienteApiService pacienteApiService)
        {
            _pacienteApiService = pacienteApiService;
        }

        // ID vindo da Rota (ex: /Edit?id=5)
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        // DTO para o formulário de atualização
        [BindProperty]
        public UpdatePacienteDto Paciente { get; set; }

        // Propriedades para exibir dados que não podem ser editados
        public string CpfDisplay { get; set; }
        public DateTime DataNascimentoDisplay { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // 1. Busca o PacienteDto completo da API
                var pacienteDto = await _pacienteApiService.GetByIdAsync(Id);

                if (pacienteDto == null)
                {
                    TempData["ErrorMessage"] = "Paciente não encontrado.";
                    return RedirectToPage("/Pacientes/Index");
                }

                // 2. Mapeia os dados do PacienteDto para o UpdatePacienteDto (formulário)
                Paciente = new UpdatePacienteDto
                {
                    NomeCompleto = pacienteDto.NomeCompleto,
                    TelefoneContato = pacienteDto.TelefoneContato
                };

                // 3. Popula as propriedades de exibição (CPF e DataNascimento não são editáveis)
                CpfDisplay = pacienteDto.Cpf;
                DataNascimentoDisplay = pacienteDto.DataNascimento;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar paciente: {ex.Message}";
                return RedirectToPage("/Pacientes/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Se a validação falhar, recarrega os campos de display
                await OnGetAsync(); // Recarrega CpfDisplay e DataNascimentoDisplay
                return Page(); // Retorna o formulário com erros
            }

            try
            {
                // Chama o serviço da API para atualizar
                await _pacienteApiService.UpdateAsync(Id, Paciente);

                TempData["SuccessMessage"] = "Paciente atualizado com sucesso!";

                // Redireciona de volta para a lista
                return RedirectToPage("/Pacientes/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao atualizar paciente: {ex.Message}");
                // Recarrega os campos de display
                await OnGetAsync();
                return Page();
            }
        }
    }
}