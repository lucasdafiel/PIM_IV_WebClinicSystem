using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebClinicSystem.Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty] // Liga esta propriedade aos campos do formulário no HTML
        public LoginInputModel Input { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            // Apenas exibe a página de login
        }

        // Este método é chamado quando o formulário é enviado (POST)
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _httpClientFactory.CreateClient();
            // ATENÇÃO: Verifique se a porta da sua API está correta aqui!
            // No seu launchSettings.json da API, a porta do perfil "https" é 7106.
            var apiAddress = "https://localhost:7106";

            var loginDto = new { Email = Input.Email, Password = Input.Password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{apiAddress}/api/auth/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // LINHA CORRIGIDA
                HttpContext.Response.Cookies.Append("AuthToken", tokenResponse.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToPage("/Home/Dashboard");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido. Verifique suas credenciais.");
                return Page();
            }
        }
    }

    // Classes auxiliares para o formulário e para receber o token
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}