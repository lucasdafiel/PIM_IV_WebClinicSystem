using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebClinicSystem.Web.Pages.Login
{
    // Representa a resposta que esperamos receber da API após um login bem-sucedido.
    public class LoginResponse
    {
        // A propriedade "token" no JSON de resposta será mapeada para esta propriedade.
        public string Token { get; set; }
    }

    public class IndexModel : PageModel
    {
        // Serviço para fazer chamadas HTTP para a nossa API.
        private readonly IHttpClientFactory _httpClientFactory;

        // Construtor que recebe o serviço IHttpClientFactory por injeção de dependência.
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // [BindProperty] faz com que os dados do formulário da página de login
        // sejam automaticamente mapeados para esta propriedade.
        [BindProperty]
        public InputModel Input { get; set; }

        // Classe que define os campos do nosso formulário de login.
        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [DataType(DataType.Password)]
            public string Senha { get; set; }
        }

        // Método executado quando a página é acessada pela primeira vez (via GET).
        public void OnGet()
        {
            // Nenhuma ação necessária aqui.
        }

        // Método executado quando o formulário de login é enviado (via POST).
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica se os dados do formulário são válidos (ex: campos obrigatórios preenchidos).
            if (!ModelState.IsValid)
            {
                return Page(); // Se inválido, recarrega a página para exibir os erros de validação.
            }

            // Cria um cliente HTTP nomeado para se comunicar com a API (usa base address de appsettings.json).
            var httpClient = _httpClientFactory.CreateClient("WebClinicSystemApi");

            // Cria um objeto anônimo com os dados de login no formato que a API espera.
            var loginData = new { email = Input.Email, password = Input.Senha };

            try
            {
                // Usa rota relativa — o HttpClient nomeado já tem BaseAddress configurada.
                var response = await httpClient.PostAsJsonAsync("api/auth/login", loginData);

                // Verifica se a API retornou um código de sucesso (2xx).
                if (response.IsSuccessStatusCode)
                {
                    // Lê o corpo da resposta como uma string JSON.
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Converte a string JSON para o nosso objeto LoginResponse,
                    // permitindo que acessemos o token.
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Ignora se o "token" no JSON é maiúsculo ou minúsculo.
                    });

                    // --- ARMAZENAMENTO DO TOKEN ---
                    if (!string.IsNullOrEmpty(loginResponse?.Token))
                    {
                        // Configura as opções do cookie.
                        var cookieOptions = new CookieOptions
                        {
                            // HttpOnly: O cookie não pode ser acessado por scripts do lado do cliente (JavaScript),
                            // o que previne ataques de XSS (Cross-Site Scripting). É uma medida de segurança crucial.
                            HttpOnly = true,
                            // Secure: O cookie só será enviado em requisições HTTPS.
                            Secure = true,
                            // SameSite.Strict: O cookie só será enviado se a requisição se originar do mesmo site.
                            // Ajuda a prevenir ataques CSRF (Cross-Site Request Forgery).
                            SameSite = SameSiteMode.Strict,
                            // Define um tempo de expiração para o cookie (ex: 1 hora).
                            Expires = DateTime.UtcNow.AddHours(1)
                        };

                        // Adiciona o token ao cookie na resposta que será enviada ao navegador do usuário.
                        Response.Cookies.Append("AuthToken", loginResponse.Token, cookieOptions);

                        // Se o login foi bem-sucedido e o token foi armazenado,
                        // redireciona o usuário para a página principal (Dashboard).
                        return RedirectToPage("/Index");
                    }

                    // Se a API retornou sucesso, mas não enviou um token, mostra um erro.
                    ModelState.AddModelError(string.Empty, "Resposta de autenticação inválida do servidor.");
                    return Page();
                }
                else
                {
                    // Se a API retornou um erro (ex: 400 Bad Request para credenciais inválidas),
                    // adiciona uma mensagem de erro genérica para o usuário.
                    ModelState.AddModelError(string.Empty, "Credenciais inválidas. Tente novamente.");
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                // Se ocorreu um erro de rede (ex: a API não está rodando), mostra um erro de conexão.
                ModelState.AddModelError(string.Empty, "Não foi possível conectar ao servidor de autenticação.");
                return Page();
            }
        }
    }
}