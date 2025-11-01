using WebClinicSystem.Application.Features.Pacientes.DTOs;
using System.Net.Http.Json;

namespace WebClinicSystem.Web.Services
{
    public class PacienteApiService : IPacienteApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // O nome "WebClinicApi" deve ser o mesmo que definimos no Program.cs (Passo 6)
        private const string HttpClientName = "WebClinicApi";

        public PacienteApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            // Cria um cliente HTTP que já tem o BaseAddress (http://localhost:porta)
            // e o AuthTokenHandler (que adiciona o token JWT) configurados.
            return _httpClientFactory.CreateClient(HttpClientName);
        }

        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/pacientes"
            var response = await client.GetAsync("api/pacientes");

            if (response.IsSuccessStatusCode)
            {
                // Lê o JSON da resposta e converte para a lista de DTOs
                var pacientes = await response.Content.ReadFromJsonAsync<IEnumerable<PacienteDto>>();
                return pacientes ?? Enumerable.Empty<PacienteDto>();
            }

            // Em um projeto real, trataríamos os erros aqui (ex: 401, 404, 500)
            return Enumerable.Empty<PacienteDto>();
        }

        public async Task<PacienteDto> GetByIdAsync(int id)
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/pacientes/{id}"
            var response = await client.GetAsync($"api/pacientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var paciente = await response.Content.ReadFromJsonAsync<PacienteDto>();
                return paciente;
            }

            return null;
        }

        public async Task CreateAsync(PacienteDto dto)
        {
            var client = CreateClient();

            // Faz a chamada POST para "api/pacientes" enviando o DTO como JSON
            var response = await client.PostAsJsonAsync("api/pacientes", dto);

            // Garante que a requisição foi bem-sucedida, senão lança uma exceção
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdatePacienteDto dto)
        {
            var client = CreateClient();

            // Faz a chamada PUT para "api/pacientes/{id}"
            var response = await client.PutAsJsonAsync($"api/pacientes/{id}", dto);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var client = CreateClient();

            // Faz a chamada DELETE para "api/pacientes/{id}"
            var response = await client.DeleteAsync($"api/pacientes/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}