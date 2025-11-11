using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Profissionais.DTOs;
using WebClinicSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebClinicSystem.Web.Pages.Profissionais
{
    [Authorize(Roles = "Administrador")]
    public class CreateModel : PageModel
    {
        private readonly IProfissionalApiService _profissionalApiService;

        public CreateModel(IProfissionalApiService profissionalApiService)
        {
            _profissionalApiService = profissionalApiService;
        }

        [BindProperty]
        public ProfissionalFormModel Profissional { get; set; } = new ProfissionalFormModel();

        public void OnGet()
        {
            // Exibe o formulário vazio
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var dto = new CreateProfissionalDto(
                    Profissional.Nome,
                    Profissional.Especialidade,
                    Profissional.CRM,
                    Profissional.Telefone
                );
                await _profissionalApiService.CreateAsync(dto);
                TempData["SuccessMessage"] = "Profissional cadastrado com sucesso!";
                return RedirectToPage("/Profissionais/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao criar profissional: {ex.Message}");
                return Page();
            }
        }
    }

    public class ProfissionalFormModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CRM é obrigatório")]
        [StringLength(20, ErrorMessage = "O CRM deve ter no máximo 20 caracteres")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória")]
        [StringLength(50, ErrorMessage = "A especialidade deve ter no máximo 50 caracteres")]
        public string Especialidade { get; set; }
    }
}
