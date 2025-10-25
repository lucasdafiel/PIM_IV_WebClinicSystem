using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebClinicSystem.Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Propriedade que irá receber os dados do formulário
        [BindProperty]
        public InputModel Input { get; set; }

        // Classe que representa os campos do formulário
        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "O Email não é um endereço válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [DataType(DataType.Password)]
            public string Senha { get; set; }
        }

        // Construtor para injetar o HttpClientFactory que configuramos no Passo 1
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Este método é executado quando a página é carregada via GET (primeiro acesso)
        public void OnGet()
        {
        }

        // ESTE MÉTODO É O MAIS IMPORTANTE
        // Ele é executado quando o usuário clica no botão "Entrar" (POST)
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica se os campos foram preenchidos corretamente (ex: email é um email válido)
            if (!ModelState.IsValid)
            {
                return Page(); // Se não for válido, recarrega a página para mostrar os erros
            }

            // Cria um cliente HTTP para chamar nossa API
            var httpClient = _httpClientFactory.CreateClient();

            // Monta o objeto com os dados de login para enviar à API
            var loginData = new { email = Input.Email, password = Input.Senha };

            try
            {
                // Envia os dados para o endpoint de login da sua API
                // ATENÇÃO: Verifique se a porta :7013 é a porta correta da sua API. 
                // Você pode conferir isso no arquivo Properties/launchSettings.json do projeto da API.
                var response = await httpClient.PostAsJsonAsync("https://localhost:7013/api/auth/login", loginData);

                // Se a API retornou sucesso (Status 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Lógica para armazenar o token (será feita em um próximo passo)

                    // COMANDO DE REDIRECIONAMENTO:
                    // Se o login deu certo, redireciona para a página principal (Dashboard)
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Se a API retornou um erro (ex: usuário ou senha inválidos)
                    ModelState.AddModelError(string.Empty, "Login inválido. Verifique suas credenciais.");
                    return Page(); // Recarrega a página de login para mostrar o erro
                }
            }
            catch (HttpRequestException)
            {
                // Se não conseguiu nem se conectar à API (ex: API não está rodando)
                ModelState.AddModelError(string.Empty, "Não foi possível conectar ao servidor. Tente novamente mais tarde.");
                return Page();
            }
        }
    }
}