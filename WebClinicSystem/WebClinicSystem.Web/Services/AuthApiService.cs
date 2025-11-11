using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebClinicSystem.Application.Features.Auth.DTOs;

namespace WebClinicSystem.Web.Services
{
    // Serviço que consome os endpoints de autenticação da API
    public class AuthApiService : IAuthApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        // Realiza o login na API e armazena o token na sessão se bem-sucedido
        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient("WebClinicSystemApi");

            try
            {
                var response = await client.PostAsJsonAsync("api/auth/login", loginDto);

                if (!response.IsSuccessStatusCode)
                    return false;

                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                if (!doc.RootElement.TryGetProperty("token", out var tokenElement))
                    return false;

                var token = tokenElement.GetString();

                if (string.IsNullOrEmpty(token))
                    return false;

                // Armazena o token na sessão
                _httpContextAccessor.HttpContext.Session.SetString("JWToken", token);

                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        // Remove o token da sessão
        public Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove("JWToken");
            return Task.CompletedTask;
        }

        public async Task<bool> RegisterUser(RegisterUserDto dto)
        {
            var client = _httpClientFactory.CreateClient("WebClinicSystemApi");
            try
            {
                var response = await client.PostAsJsonAsync("api/auth/register", dto);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
