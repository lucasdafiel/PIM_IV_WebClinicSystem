using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

// Criamos um "apelido" para a classe Profissional
using ProfissionalEntity = WebClinicSystem.Domain.Entities.Profissional;

namespace WebClinicSystem.Web.Pages.Profissionais
{
    public class IndexModel : PageModel
    {
        public List<ProfissionalEntity> ListaDeProfissionais { get; set; }

        public void OnGet()
        {
            ListaDeProfissionais = new List<ProfissionalEntity>();
        }
    }
}