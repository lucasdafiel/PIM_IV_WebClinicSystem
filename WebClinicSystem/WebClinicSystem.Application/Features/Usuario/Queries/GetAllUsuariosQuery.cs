using MediatR;
using System.Collections.Generic;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Application.Features.Usuarios.Queries
{
    // Query para buscar todos os usuários
    // Retorna uma coleção (IEnumerable) de UsuarioDto
    public class GetAllUsuariosQuery : IRequest<IEnumerable<UsuarioDto>>
    {
        // Esta query não precisa de parâmetros
    }
}