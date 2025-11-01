using WebClinicSystem.Application.Features.Consultas.DTOs;
using System.Net.Http.Json;
using System.Web; // Adicione este using para o HttpUtility

namespace WebClinicSystem.Web.Services
{
    public class ConsultaApiService : IConsultaApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string HttpClientName = "WebClinicApi";

        public ConsultaApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient(HttpClientName);
        }

        public async Task<IEnumerable<ConsultaDTO>> GetAllAsync(int? idProfissional, DateTime? dataInicio, DateTime? dataFim)
        {
            var client = CreateClient();

            // Precisamos construir a URL com os filtros (query parameters)
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            if (idProfissional.HasValue)
                queryString["idProfissional"] = idProfissional.Value.ToString();
            if (dataInicio.HasValue)
                queryString["dataInicio"] = dataInicio.Value.ToString("o"); // Formato ISO 8601
            if (dataFim.HasValue)
                queryString["dataFim"] = dataFim.Value.ToString("o");

            var url = $"api/consultas?{queryString}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var consultas = await response.Content.ReadFromJsonAsync<IEnumerable<ConsultaDTO>>();
                return consultas ?? Enumerable.Empty<ConsultaDTO>();
            }

            return Enumerable.Empty<ConsultaDTO>();
        }

        public async Task AgendarAsync(CreateConsultaDTO dto)
        {
            var client = CreateClient();

            // Faz a chamada POST para "api/consultas"
            var response = await client.PostAsJsonAsync("api/consultas", dto);

            response.EnsureSuccessStatusCode();
        }

        public async Task CancelarAsync(int id)
        {
            var client = CreateClient();

            // Faz a chamada PUT para "api/consultas/{id}/cancelar"
            // Como o endpoint não espera um corpo, passamos null
            var response = await client.PutAsync($"api/consultas/{id}/cancelar", null);

            response.EnsureSuccessStatusCode();
        }

        public async Task<RelatorioConsultasDTO> GetRelatorioAsync(DateTime dataInicio, DateTime dataFim, int? idProfissional)
        {
            var client = CreateClient();

            // Constrói a query string para o relatório
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["dataInicio"] = dataInicio.ToString("o");
            queryString["dataFim"] = dataFim.ToString("o");
            if (idProfissional.HasValue)
                queryString["idProfissional"] = idProfissional.Value.ToString();

            var url = $"api/consultas/relatorio?{queryString}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RelatorioConsultasDTO>();
            }

            return null; // Ou um DTO de relatório vazio
        }
    }
}