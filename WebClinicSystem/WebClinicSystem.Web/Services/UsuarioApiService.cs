using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; // Para métodos como GetFromJsonAsync e PutAsJsonAsync
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Web.Services
{
    // Implementa a interface
    public class UsuarioApiService : IUsuarioApiService
    {
        // Injeção do IHttpClientFactory (como em ProfissionalApiService)
        private readonly IHttpClientFactory _httpClientFactory;

        // Constante para o nome do HttpClient configurado no Program.cs
        private const string ApiClientName = "WebClinicApi";

        // Constante para a rota base do controller
        private const string ApiBasePath = "api/usuarios";

        public UsuarioApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Método auxiliar privado para criar o cliente HTTP
        // (Exatamente como em ProfissionalApiService)
        private HttpClient CreateClient()
        {
            // Cria um cliente HTTP usando o nome registrado
            // Isso garante que ele receba a BaseAddress (ex: https://localhost:7001)
            // e o AuthTokenHandler (para enviar o JWT)
            return _httpClientFactory.CreateClient(ApiClientName);
        }

        #region Implementação dos Métodos

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/usuarios"
            // GetFromJsonAsync lida automaticamente com a desserialização do JSON
            return await client.GetFromJsonAsync<IEnumerable<UsuarioDto>>(ApiBasePath);
        }

        public async Task<UsuarioDto> GetByIdAsync(Guid id)
        {
            var client = CreateClient();

            // Faz a chamada GET para "api/usuarios/{id}"
            return await client.GetFromJsonAsync<UsuarioDto>($"{ApiBasePath}/{id}");
        }

        public async Task UpdateAsync(Guid id, UpdateUsuarioDto dto)
        {
            var client = CreateClient();

            // Faz a chamada PUT para "api/usuarios/{id}"
            // PutAsJsonAsync serializa o 'dto' para JSON e o envia no corpo
            var response = await client.PutAsJsonAsync($"{ApiBasePath}/{id}", dto);

            // Garante que a resposta foi bem-sucedida (status 2xx)
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = CreateClient();

            // Faz a chamada DELETE para "api/usuarios/{id}"
            var response = await client.DeleteAsync($"{ApiBasePath}/{id}");

            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}