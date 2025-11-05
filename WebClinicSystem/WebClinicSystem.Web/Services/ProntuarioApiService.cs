using System.Net.Http;
using System.Net.Http.Json; // Necessário para GetFromJsonAsync e PostAsJsonAsync
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;

namespace WebClinicSystem.Web.Services
{
    public class ProntuarioApiService : IProntuarioApiService
    {
        // 1. Injeção do IHttpClientFactory (padrão)
        private readonly IHttpClientFactory _httpClientFactory;

        // 2. Constantes para o cliente e a rota base
        private const string ApiClientName = "WebClinicApi";
        private const string ApiBasePath = "api/prontuarios";

        public ProntuarioApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 3. Método auxiliar para criar o cliente HTTP (padrão)
        // Garante que o cliente tenha a BaseAddress e o AuthTokenHandler
        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient(ApiClientName);
        }

        #region Implementação dos Métodos

        /// <summary>
        /// Implementação do CreateAsync.
        /// Faz uma chamada POST para "api/prontuarios"
        /// </summary>
        public async Task CreateAsync(CreateProntuarioDTO dto)
        {
            var client = CreateClient();

            // Serializa o 'dto' e envia como JSON no corpo da requisição POST
            var response = await client.PostAsJsonAsync(ApiBasePath, dto);

            // Garante que a API retornou sucesso (status 2xx)
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Implementação do GetByConsultaIdAsync.
        /// Faz uma chamada GET para "api/prontuarios/consulta/{idConsulta}"
        /// </summary>
        public async Task<ProntuarioDTO> GetByConsultaIdAsync(int idConsulta)
        {
            var client = CreateClient();

            // Constrói a rota específica
            var path = $"{ApiBasePath}/consulta/{idConsulta}";

            // Faz a chamada GET e desserializa a resposta para ProntuarioDTO
            // Nota: Se o ProntuariosController retornar Ok(null), 
            // este método também retornará null, que é o esperado.
            return await client.GetFromJsonAsync<ProntuarioDTO>(path);
        }

        #endregion
    }
}