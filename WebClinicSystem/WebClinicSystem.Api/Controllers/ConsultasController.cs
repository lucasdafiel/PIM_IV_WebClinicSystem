using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Consultas.Commands;
using WebClinicSystem.Application.Features.Consultas.DTOs;
using WebClinicSystem.Application.Features.Consultas.Queries;

namespace WebClinicSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Todos precisam estar autenticados
    public class ConsultasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Endpoint para agendar consulta (apenas Administrador e Recepcionista)
        [HttpPost]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> AgendarConsulta([FromBody] CreateConsultaDTO dto)
        {
            var command = new AgendarConsultaCommand(dto);
            var consultaId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllConsultas), new { id = consultaId }, new { id = consultaId });
        }

        // Endpoint para cancelar consulta (apenas Administrador e Recepcionista)
        [HttpPut("{id}/cancelar")]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> CancelarConsulta(int id)
        {
            var command = new CancelarConsultaCommand(id);
            var resultado = await _mediator.Send(command);
            if (!resultado)
                return NotFound();
            return NoContent();
        }

        // Endpoint para listar todas as consultas (qualquer perfil autenticado)
        [HttpGet]
        public async Task<IActionResult> GetAllConsultas()
        {
            var query = new GetAllConsultasQuery();
            var consultas = await _mediator.Send(query);
            return Ok(consultas);
        }

        // Endpoint para relatório de consultas (apenas Administrador)
        [HttpGet("relatorio")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetRelatorioConsultas([FromQuery] GetRelatorioConsultasQuery query)
        {
            // Chama a query para gerar o relatório
            var relatorio = await _mediator.Send(query);
            return Ok(relatorio);
        }
    }
}