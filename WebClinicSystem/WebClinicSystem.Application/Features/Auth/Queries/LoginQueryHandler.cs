using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Auth.Queries
{
    // Handler que executa a lógica para a query de Login.
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        // Dependência para acessar o banco de dados.
        private readonly IUnitOfWork _unitOfWork;
        // Dependência para usar os serviços de autenticação (verificar senha, gerar token).
        private readonly IAuthService _authService;

        // Construtor que recebe as dependências necessárias.
        public LoginQueryHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        // Método principal que executa a lógica de login.
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // 1. Busca o usuário no banco de dados pelo e-mail fornecido.
            var user = await _unitOfWork.Usuarios.GetByEmailAsync(request.Email);

            // 2. Verifica se o usuário não foi encontrado.
            if (user == null)
            {
                // Lança um erro se o e-mail não existir, para barrar a tentativa de login.
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            // 3. Usa o serviço de autenticação para comparar a senha enviada com o hash salvo no banco.
            var isPasswordCorrect = _authService.VerifyPassword(request.Password, user.SenhaHash);

            // 4. Verifica se a senha está incorreta.
            if (!isPasswordCorrect)
            {
                // Lança um erro se a senha estiver errada.
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            // 5. Se o usuário e a senha estiverem corretos, gera o token JWT.
            var token = _authService.GenerateJwtToken(user);

            // 6. Retorna o token como resultado da operação.
            return token;
        }
    }
}
