using MediatR;
using System;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Application.Features.Usuarios.Commands
{
    // Command para atualizar um usuário existente
    // Retorna o UsuarioDto atualizado
    public class UpdateUsuarioCommand : IRequest<UsuarioDto>
    {
        public Guid Id { get; set; } // ID do usuário a ser atualizado (da rota)
        public UpdateUsuarioDto Dto { get; set; } // Dados da atualização (do corpo)
    }
}