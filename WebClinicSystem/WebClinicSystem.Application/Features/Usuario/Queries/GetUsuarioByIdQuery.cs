using MediatR;
using System;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Application.Features.Usuarios.Queries
{
    // Query para buscar um usuário por seu ID
    // Retorna um único UsuarioDto
    public class GetUsuarioByIdQuery : IRequest<UsuarioDto>
    {
        public Guid Id { get; set; }
    }
}