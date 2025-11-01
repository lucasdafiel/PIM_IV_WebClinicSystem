using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebClinicSystem.Web.HttpHandlers
{
    // Handler que injeta o token JWT armazenado na sessão em requisições HTTP
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Injeta IHttpContextAccessor para acessar a sessão do usuário atual
        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Sobrescreve o envio das requisições para adicionar o header Authorization quando houver token na sessão
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Recupera o token JWT armazenado na sessão (se existir)
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

            // Se houver token, adiciona o header Authorization Bearer
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Continua o pipeline de handlers
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
