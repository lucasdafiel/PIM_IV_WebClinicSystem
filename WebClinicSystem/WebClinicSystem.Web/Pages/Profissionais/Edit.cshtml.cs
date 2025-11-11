using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Profissionais.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Profissionais
{
    [Authorize(Roles = "Administrador")]
    public class EditModel : PageModel
    {
        // 1. Injeção de Dependência
        // O serviço da API é injetado para podermos consumir os endpoints.
        private readonly IProfissionalApiService _profissionalApiService;

        // 2. Propriedades de Binding

        /// <summary>
        /// DTO que carrega os dados do formulário na submissão (POST).
        /// </summary>
        [BindProperty]
        public UpdateProfissionalDto Profissional { get; set; }

        /// <summary>
        /// Armazena o ID do profissional vindo da rota (GET) e 
        /// o mantém no formulário (via input hidden) para o POST.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        /// <summary>
        /// Propriedade para exibir o CRM (RegistroConselho), 
        /// que não é editável neste formulário.
        /// </summary>
        public string RegistroConselho { get; set; }

        public EditModel(IProfissionalApiService profissionalApiService)
        {
            _profissionalApiService = profissionalApiService;
        }

        // 3. Método OnGetAsync
        // Este método é executado quando a página é carregada.
        // Ele busca o profissional pelo 'id' e preenche o formulário.
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Armazena o ID vindo da rota
            Id = id;

            // Busca os dados completos do profissional na API
            var profissionalDto = await _profissionalApiService.GetByIdAsync(id);

            if (profissionalDto == null)
            {
                // Se não encontrar, retorna 404
                return NotFound();
            }

            // Mapeia os dados do ProfissionalDto (retorno) 
            // para o UpdateProfissionalDto (formulário)
            // Nota: Usamos 'new' porque DTOs baseados em 'record' são imutáveis.
            Profissional = new UpdateProfissionalDto(
                profissionalDto.NomeCompleto,
                profissionalDto.Especialidade,
                profissionalDto.Telefone
            );

            // Preenche a propriedade separada para o CRM (RegistroConselho)
            RegistroConselho = profissionalDto.RegistroConselho;

            return Page();
        }

        // 4. Método OnPostAsync
        // Este método é executado quando o formulário é submetido (via <form method="post">).
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica se os dados do formulário são válidos
            // (ex: campos obrigatórios preenchidos, se houver validações no DTO)
            if (!ModelState.IsValid)
            {
                // Recarrega o CRM para exibição, pois ele não é "bindado" no POST
                var profissionalDto = await _profissionalApiService.GetByIdAsync(Id);
                RegistroConselho = profissionalDto.RegistroConselho;

                // Retorna a página com os dados atuais e as mensagens de erro
                return Page();
            }

            try
            {
                // Envia a atualização para a API, passando o ID e o DTO
                await _profissionalApiService.UpdateAsync(Id, Profissional);
            }
            catch (Exception ex)
            {
                // TODO: Implementar log de erro
                // Retorna a página com uma mensagem de erro (opcional)
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o profissional.");

                var profissionalDto = await _profissionalApiService.GetByIdAsync(Id);
                RegistroConselho = profissionalDto.RegistroConselho;

                return Page();
            }

            // 5. Redirecionamento
            // Se tudo der certo, redireciona para a página de listagem
            return RedirectToPage("/Profissionais/Index");
        }
    }
}