using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        //Método para computar o hash SHA-256 de uma senha.
        public string ComputeSha256Hash(string password)
        {
            // Usando BCrypt para hashear a senha.
            // BCrypt incorpora um salt automaticamente e é uma escolha segura para armazenamento de senhas.
            return BCrypt.Net.BCrypt.HashPassword(password);    
        }
    }
}
