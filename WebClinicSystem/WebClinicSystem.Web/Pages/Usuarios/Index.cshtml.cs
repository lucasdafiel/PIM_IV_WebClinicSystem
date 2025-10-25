using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

// Criamos um "apelido" para a classe Usuario
using UsuarioEntity = WebClinicSystem.Domain.Entities.Usuario;

namespace WebClinicSystem.Web.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        public List<UsuarioEntity> ListaDeUsuarios { get; set; }

        public void OnGet()
        {
            ListaDeUsuarios = new List<UsuarioEntity>();
        }
    }
}