using MediatR;
using System;

namespace WebClinicSystem.Application.Features.Usuarios.Commands
{
    // Command para deletar um usuário
    // Retorna 'Unit' (o equivalente a 'void' para o MediatR)
    public class DeleteUsuarioCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}