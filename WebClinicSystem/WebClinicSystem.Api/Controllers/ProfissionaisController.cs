using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Application.Features.Profissionais.Commands;
using WebClinicSystem.Application.Features.Profissionais.DTOs;
using WebClinicSystem.Application.Features.Profissionais.Queries;

[ApiController]
[Route("api/[controller]")]
public class ProfissionaisController : ControllerBase
{
    private readonly ISender _mediator;

    public ProfissionaisController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfissional([FromBody] CreateProfissionalDto dto)
    {
        var command = new CreateProfissionalCommand(dto.NomeCompleto, dto.Especialidade, dto.RegistroConselho, dto.Telefone);
        var profissionalId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProfissionalById), new { id = profissionalId }, new { id = profissionalId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfissionais()
    {
        var query = new GetAllProfissionaisQuery();
        var profissionais = await _mediator.Send(query);
        return Ok(profissionais);
    }

    // Endpoint para Buscar um Profissional por ID (GET)
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProfissionalById(int id)
    {
        // Cria a query com o ID recebido na rota.
        var query = new GetProfissionalByIdQuery(id);

        // Envia a query para o MediatR.
        var profissional = await _mediator.Send(query);

        // Se o profissional for encontrado, retorna 200 OK com os dados.
        // Caso contrário, retorna 404 Not Found.
        return profissional is not null ? Ok(profissional) : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProfissional(int id, [FromBody] UpdateProfissionalDto dto)
    {
        var command = new UpdateProfissionalCommand(id, dto.NomeCompleto, dto.Especialidade, dto.Telefone);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProfissional(int id)
    {
        var command = new DeleteProfissionalCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}