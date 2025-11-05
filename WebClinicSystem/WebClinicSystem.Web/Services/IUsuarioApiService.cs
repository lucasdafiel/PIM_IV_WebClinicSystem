using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Web.Services
{
    public interface IUsuarioApiService
    {
        // Métodos do CRUD (sem o Create, conforme solicitado)

        Task<IEnumerable<UsuarioDto>> GetAllAsync();

        // Corrigido para Guid para corresponder ao ID do IdentityUser/UsuariosController
        Task<UsuarioDto> GetByIdAsync(Guid id);

        // Corrigido para Guid
        Task UpdateAsync(Guid id, UpdateUsuarioDto dto);

        // Corrigido para Guid
        Task DeleteAsync(Guid id);
    }
}