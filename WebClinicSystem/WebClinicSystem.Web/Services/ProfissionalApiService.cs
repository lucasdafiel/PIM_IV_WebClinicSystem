using WebClinicSystem.Application.Features.Profissionais.DTOs;
using System.Net.Http.Json;

namespace WebClinicSystem.Web.Services
{
    public class ProfissionalApiService : IProfissionalApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string HttpClientName = "WebClinicApi"; // O mesmo nome do Program.cs

        public ProfissionalApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            // Este cliente já vem configurado com a BaseAddress e o Token JWT
            return _httpClientFactory.CreateClient(HttpClientName);
        }

        public async Task<IEnumerable<ProfissionalDto>> GetAllAsync()
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/profissionais"
            var response = await client.GetAsync("api/profissionais");

            if (response.IsSuccessStatusCode)
            {
                var profissionais = await response.Content.ReadFromJsonAsync<IEnumerable<ProfissionalDto>>();
                return profissionais ?? Enumerable.Empty<ProfissionalDto>();
            }

            return Enumerable.Empty<ProfissionalDto>();
        }

        public async Task<ProfissionalDto> GetByIdAsync(int id)
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/profissionais/{id}"
            var response = await client.GetAsync($"api/profissionais/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProfissionalDto>();
            }

            return null;
        }

        public async Task CreateAsync(CreateProfissionalDto dto)
        {
            var client = CreateClient();

            // Faz a chamada POST para "api/profissionais"
            var response = await client.PostAsJsonAsync("api/profissionais", dto);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdateProfissionalDto dto)
        {
            var client = CreateClient();

            // Faz a chamada PUT para "api/profissionais/{id}"
            var response = await client.PutAsJsonAsync($"api/profissionais/{id}", dto);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var client = CreateClient();

            // Faz a chamada DELETE para "api/profissionais/{id}"
            var response = await client.DeleteAsync($"api/profissionais/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}