using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Auth.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Usuarios
{
    [Authorize(Roles = "Administrador")]
    public class CreateModel : PageModel
    {
        private readonly IAuthApiService _authApiService;
        private readonly IUsuarioApiService _usuarioApiService;

        public CreateModel(IAuthApiService authApiService, IUsuarioApiService usuarioApiService)
        {
            _authApiService = authApiService;
            _usuarioApiService = usuarioApiService;
        }

        [BindProperty]
        public RegisterUserDto RegisterUserDto { get; set; }

        public SelectList PerfisSelectList { get; set; }

        public async Task OnGetAsync()
        {
            var perfis = await _usuarioApiService.GetPerfis();
            PerfisSelectList = new SelectList(perfis, "Id", "Nome");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Recarrega PerfisSelectList
                return Page();
            }

            var result = await _authApiService.RegisterUser(RegisterUserDto);
            if (result)
            {
                return RedirectToPage("/Usuarios/Index");
            }

            ModelState.AddModelError(string.Empty, "Erro ao registrar usuário.");
            await OnGetAsync();
            return Page();
        }
    }
}
