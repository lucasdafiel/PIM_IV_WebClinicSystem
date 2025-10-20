using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Auth.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Criptografa a senha recebida usando nosso serviço de autenticação.
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            // 2. Cria a nova entidade Usuario.
            var user = new Usuario
            {
                Email = request.Email,
                SenhaHash = passwordHash,
                PerfilId = request.PerfilId,
                Ativo = true // Por padrão, novos usuários são criados como ativos.
            };

            // 3. Adiciona o usuário ao repositório e salva no banco.
            await _unitOfWork.Usuarios.AddAsync(user); // Precisaremos criar este repositório
            await _unitOfWork.CompleteAsync();

            return user.UsuarioId;
        }
    }
}
