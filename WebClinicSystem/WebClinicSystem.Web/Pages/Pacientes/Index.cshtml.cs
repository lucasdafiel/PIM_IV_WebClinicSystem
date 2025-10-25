using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebClinicSystem.Domain.Entities; // Agora vai funcionar!

namespace WebClinicSystem.Web.Pages.Pacientes // Namespace corrigido
{
    public class IndexModel : PageModel
    {
        public List<Paciente> ListaDePacientes { get; set; }

        public void OnGet()
        {
            ListaDePacientes = new List<Paciente>();
        }
    }
}