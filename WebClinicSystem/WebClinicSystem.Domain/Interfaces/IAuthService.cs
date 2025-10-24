using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IAuthService
    {
        //Método para computar o hash SHA-256 de uma senha.
        string ComputeSha256Hash(string password);
        //Método para gerar um token JWT para um usuário autenticado.
        string GenerateJwtToken(Usuario user);
        //Método para verificar se uma senha corresponde a um hash armazenado.
        bool VerifyPassword(string password, string hash);
    }
}
