using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;
using WebClinicSystem.Web.Services;

namespace WebClinicSystem.Web.Pages.Prontuario
{
    [Authorize(Roles = "Administrador,Profissional de Saúde")]
    public class CreateModel : PageModel
    {
        // 1. Injeção de Dependência
        // Injetamos os serviços necessários para esta página.
        private readonly IProntuarioApiService _prontuarioApiService;
        private readonly IConsultaApiService _consultaApiService;

        // 2. Propriedades de Binding

        /// <summary>
        /// DTO que carrega os dados do formulário na submissão (POST).
        /// Contém a Descrição e o IdConsulta (via hidden input).
        /// </summary>
        [BindProperty]
        public CreateProntuarioDTO CreateProntuarioDTO { get; set; }

        /// <summary>
        /// DTO (sem bind) usado apenas para exibir os detalhes
        /// da consulta na tela (Nome do Paciente, Profissional, etc.).
        /// </summary>
        public ConsultaDTO Consulta { get; set; }

        public CreateModel(IProntuarioApiService prontuarioApiService, IConsultaApiService consultaApiService)
        {
            _prontuarioApiService = prontuarioApiService;
            _consultaApiService = consultaApiService;

            // Inicializa as propriedades para evitar 'null reference'
            CreateProntuarioDTO = new CreateProntuarioDTO();
            Consulta = new ConsultaDTO();
        }

        // 3. Método OnGetAsync
        // Este método é executado quando a página é carregada.
        // Ele busca a consulta pelo 'consultaId' vindo da URL.
        public async Task<IActionResult> OnGetAsync(int consultaId)
        {
            if (consultaId <= 0)
            {
                return NotFound();
            }

            // **ASSUMINDO** que você possui o método GetByIdAsync
            // Se este método não existir na sua IConsultaApiService, 
            // você precisará adicioná-lo.
            Consulta = await _consultaApiService.GetByIdAsync(consultaId);

            if (Consulta == null)
            {
                return NotFound("Consulta não encontrada.");
            }

            // Popula o DTO que será enviado no POST com o ID da consulta
            CreateProntuarioDTO.IdConsulta = consultaId;

            return Page();
        }

        // 4. Método OnPostAsync
        // Executado quando o formulário é submetido.
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica se os dados do formulário são válidos 
            // (ex: se a Descrição foi preenchida)
            if (!ModelState.IsValid)
            {
                // Se o modelo for inválido, precisamos recarregar os dados
                // da consulta para exibir a página novamente.
                Consulta = await _consultaApiService.GetByIdAsync(CreateProntuarioDTO.IdConsulta);

                if (Consulta == null)
                {
                    // Adiciona um erro se a consulta não for mais encontrada
                    ModelState.AddModelError(string.Empty, "Erro ao recarregar dados da consulta.");
                }

                return Page();
            }

            try
            {
                // Envia o DTO (com IdConsulta e Descricao) para a API criar o prontuário
                await _prontuarioApiService.CreateAsync(CreateProntuarioDTO);
            }
            catch (Exception ex)
            {
                // TODO: Implementar log de erro
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o prontuário.");

                // Recarrega os dados da consulta em caso de falha
                Consulta = await _consultaApiService.GetByIdAsync(CreateProntuarioDTO.IdConsulta);
                return Page();
            }

            // 5. Redirecionamento
            // Se tudo der certo, redireciona para a Agenda
            return RedirectToPage("/Agenda/Index");
        }
    }
}