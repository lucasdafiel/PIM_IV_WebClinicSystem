using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Usuarios.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Usuarios
{
    // 1. Adiciona a diretiva [Authorize(Roles = "Administrador")]
    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        // 2. Injeta o novo serviço 'IUsuarioApiService'
        private readonly IUsuarioApiService _usuarioApiService;

        public IndexModel(IUsuarioApiService usuarioApiService)
        {
            _usuarioApiService = usuarioApiService;
        }

        // 3. Propriedade 'public IList<UsuarioDto> Usuarios { get; set; }'
        // (Usando UsuarioDto, conforme DTOs que criamos)
        public IList<UsuarioDto> Usuarios { get; set; } = new List<UsuarioDto>();

        // 4. No método OnGetAsync(), chama '_usuarioApiService.GetAllAsync()'
        public async Task OnGetAsync()
        {
            // Busca os dados da API
            var usuariosEnumerable = await _usuarioApiService.GetAllAsync();

            // Popula a lista (seguindo o exemplo de Profissionais/Index.cshtml.cs)
            if (usuariosEnumerable != null)
            {
                Usuarios = usuariosEnumerable.ToList();
            }
        }
    }
}