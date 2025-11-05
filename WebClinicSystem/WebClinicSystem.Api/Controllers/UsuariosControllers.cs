using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// Importações presumidas para os DTOs, Commands e Queries
// (Estes namespaces podem precisar de ajuste dependendo da estrutura da sua camada Application)
using WebClinicSystem.Application.Features.Usuarios.Commands;
using WebClinicSystem.Application.Features.Usuarios.Queries;
using WebClinicSystem.Application.Features.Usuarios.DTOs;

namespace WebClinicSystem.Api.Controllers
{
    // 2. Protegido com [Authorize(Roles = "Administrador")]
    [Authorize(Roles = "Administrador")]
    // 1. Rota [Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Injeção do MediatR (como no ProfissionaisController)
        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 3. Endpoint [HttpGet] GetAllUsuarios
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            // Assume que existe uma query 'GetAllUsuariosQuery'
            var query = new GetAllUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return Ok(usuarios);
        }

        // 3. Endpoint [HttpGet("{id}")] GetUsuarioById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id) // Usuários geralmente usam Guid como ID
        {
            // Assume que existe uma query 'GetUsuarioByIdQuery'
            var query = new GetUsuarioByIdQuery { Id = id };
            var usuario = await _mediator.Send(query);

            return usuario != null ? Ok(usuario) : NotFound();
        }

        // 3. Endpoint [HttpPut("{id}")] UpdateUsuario
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(Guid id, [FromBody] UpdateUsuarioDto dto)
        {
            // Assume que existe um command 'UpdateUsuarioCommand' e um 'UpdateUsuarioDto'
            var command = new UpdateUsuarioCommand
            {
                Id = id,
                // Mapeia outras propriedades do DTO para o Command, se necessário
                // Ex: Email = dto.Email, PerfilId = dto.PerfilId, etc.
                Dto = dto // Exemplo de passagem do DTO
            };

            var usuarioAtualizado = await _mediator.Send(command);

            return usuarioAtualizado != null ? Ok(usuarioAtualizado) : NotFound();
        }

        // 3. Endpoint [HttpDelete("{id}")] DeleteUsuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            // Assume que existe um command 'DeleteUsuarioCommand'
            var command = new DeleteUsuarioCommand { Id = id };
            await _mediator.Send(command);

            return NoContent(); // Resposta padrão para Delete bem-sucedido
        }
    }
}