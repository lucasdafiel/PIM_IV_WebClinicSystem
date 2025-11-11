using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Usuarios.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Usuarios
{
    [Authorize(Roles = "Administrador")]
    public class EditModel : PageModel
    {
        private readonly IUsuarioApiService _usuarioApiService;

        public EditModel(IUsuarioApiService usuarioApiService)
        {
            _usuarioApiService = usuarioApiService;
        }

        [BindProperty]
        public UpdateUsuarioDto Usuario { get; set; }

        public SelectList PerfisSelectList { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Id = id;

                var usuarioDto = await _usuarioApiService.GetUsuarioById(id);
                if (usuarioDto == null)
                {
                    TempData["ErrorMessage"] = "Usuário não encontrado.";
                    return RedirectToPage("/Usuarios/Index");
                }

                Usuario = new UpdateUsuarioDto
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    PerfilId = usuarioDto.PerfilId
                };

                var perfis = await _usuarioApiService.GetPerfis();
                PerfisSelectList = new SelectList(perfis, "Id", "Nome", Usuario.PerfilId);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar usuário: {ex.Message}";
                return RedirectToPage("/Usuarios/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var perfis = await _usuarioApiService.GetPerfis();
                PerfisSelectList = new SelectList(perfis, "Id", "Nome", Usuario?.PerfilId);
                return Page();
            }

            try
            {
                await _usuarioApiService.UpdateUsuario(Id, Usuario);
                TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
                return RedirectToPage("/Usuarios/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao atualizar usuário: {ex.Message}");
                var perfis = await _usuarioApiService.GetPerfis();
                PerfisSelectList = new SelectList(perfis, "Id", "Nome", Usuario?.PerfilId);
                return Page();
            }
        }
    }
}
