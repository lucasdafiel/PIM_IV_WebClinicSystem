using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Application.Features.Auth.Commands;
using WebClinicSystem.Application.Features.Auth.DTOs;
using WebClinicSystem.Application.Features.Auth.Queries;

namespace WebClinicSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // A rota base será "api/auth"
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        // --- Endpoint para Registrar um Novo Usuário (POST) ---
        [HttpPost("register")] // Rota completa: POST api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            // Mapeia os dados do DTO para o comando de registro.
            var command = new RegisterUserCommand(dto.Email, dto.Password, dto.PerfilId);

            // Envia o comando para o handler correspondente.
            var userId = await _mediator.Send(command);

            // Retorna 201 Created com o ID do novo usuário.
            return StatusCode(201, new { id = userId });
        }

        // --- Endpoint para Autenticar um Usuário (POST) ---
        [HttpPost("login")] // Rota completa: POST api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var query = new LoginQuery(dto.Email, dto.Password);
                var token = await _mediator.Send(query);
                return Ok(new { token }); // Retorna o token em um objeto JSON.
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciais inválidas."); // Retorna 401 Unauthorized se o login falhar.
            }
        }
    }
}
