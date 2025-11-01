using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Auth.DTOs;

namespace WebClinicSystem.Web.Services
{
    // Interface do serviço que consome os endpoints de autenticação da API
    public interface IAuthApiService
    {
        // Envia as credenciais para a API e realiza o login, retornando true se bem-sucedido
        Task<bool> LoginAsync(LoginDto loginDto);

        // Realiza o logout no cliente (pode limpar cookies/sessão)
        Task LogoutAsync();
    }
}
