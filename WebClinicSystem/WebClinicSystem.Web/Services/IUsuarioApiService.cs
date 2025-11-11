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

        // Adicionado para dropdown de perfis
        Task<IEnumerable<PerfilDto>> GetPerfis();

        // Wrappers com nomes em pt-br solicitados pela UI
        Task<UsuarioDto> GetUsuarioById(Guid id);
        Task UpdateUsuario(Guid id, UpdateUsuarioDto dto);
    }
}