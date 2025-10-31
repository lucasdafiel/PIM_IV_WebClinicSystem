using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Prontuarios.Commands;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;
using WebClinicSystem.Application.Features.Prontuarios.Queries;

namespace WebClinicSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProntuariosController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProntuariosController(ISender mediator)
        {
            _mediator = mediator;
        }

        // Endpoint para criar um prontuário - apenas Profissionais de Saúde podem acessar
        [HttpPost]
        [Authorize(Roles = "Profissional de Saúde")]
        public async Task<IActionResult> CriarProntuario([FromBody] CreateProntuarioDTO dto)
        {
            var command = new CriarProntuarioCommand(dto);
            var prontuarioId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProntuarioByConsulta), new { idConsulta = dto.IdConsulta }, new { prontuarioId = prontuarioId });
        }

        // Endpoint para obter prontuário por Id da consulta - Profissional de Saúde e Administrador
        [HttpGet("consulta/{idConsulta}")]
        [Authorize(Roles = "Profissional de Saúde, Administrador")]
        public async Task<IActionResult> GetProntuarioByConsulta(int idConsulta)
        {
            var query = new GetProntuarioByConsultaIdQuery(idConsulta);
            var prontuario = await _mediator.Send(query);
            return prontuario == null ? NotFound() : Ok(prontuario);
        }
    }
}
