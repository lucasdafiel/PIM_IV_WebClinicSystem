using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Auth.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        // Serviço que realiza autenticação via API
        private readonly IAuthApiService _authApiService;

        // Injeta o serviço de autenticação
        public IndexModel(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        // DTO de login ligado ao formulário
        [BindProperty]
        public LoginDto LoginDto { get; set; }

        // Exibe a página de login
        public void OnGet()
        {
        }

        // Recebe o POST do formulário de login e delega para o serviço de autenticação
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loginSucesso = await _authApiService.LoginAsync(LoginDto);
            if (loginSucesso)
            {
                // Se o login funcionar, redireciona para o Dashboard
                return RedirectToPage("/Home/Dashboard");
            }

            // Se falhar, mostra um erro
            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            return Page();
        }
    }
}