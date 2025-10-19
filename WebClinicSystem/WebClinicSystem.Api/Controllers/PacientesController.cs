using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Application.Features.Pacientes.Commands;
using WebClinicSystem.Application.Features.Pacientes.DTOs;
using WebClinicSystem.Application.Features.Pacientes.Queries;

[ApiController] // Define que esta classe é um Controller de API.
[Route("api/[controller]")] // Define a rota base como "api/pacientes".
public class PacientesController : ControllerBase
{
    // Dependência do MediatR para enviar comandos e queries para a camada de Application.
    private readonly ISender _mediator;

    // O construtor recebe o MediatR via injeção de dependência.
    public PacientesController(ISender mediator)
    {
        _mediator = mediator;
    }

    // --- Endpoint para Cadastrar um Novo Paciente (POST) ---
    [HttpPost]
    public async Task<IActionResult> CreatePaciente([FromBody] PacienteDto pacienteDto)
    {
        // 1. Mapeia os dados do DTO (que veio da requisição) para o nosso Comando.
        var command = new CadastrarPacienteCommand(
            pacienteDto.NomeCompleto,
            pacienteDto.Cpf,
            pacienteDto.DataNascimento,
            pacienteDto.TelefoneContato
        );

        // 2. Envia o comando para o MediatR, que vai encontrar e executar o Handler correspondente.
        var pacienteId = await _mediator.Send(command);

        // 3. Retorna uma resposta HTTP 201 Created.
        // Isso indica que o recurso foi criado com sucesso e informa a URL para acessá-lo.
        return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteId }, new { id = pacienteId });
    }

    // --- Endpoint para Buscar um Paciente por ID (GET) ---
    // Vamos adicionar um endpoint de busca para que a URL do CreatedAtAction funcione.
    // --- Endpoint para Buscar um Paciente por ID (GET) ---
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPacienteById(int id)
    {
        // Cria a query com o ID recebido na rota.
        var query = new GetPacienteByIdQuery(id);

        // Envia a query para o MediatR.
        var paciente = await _mediator.Send(query);

        // Se o paciente for encontrado, retorna 200 OK com os dados. Senão, retorna 404 Not Found.
        return paciente is not null ? Ok(paciente) : NotFound();
    }

    // --- Endpoint para Listar Todos os Pacientes (GET) ---
    [HttpGet]
    public async Task<IActionResult> GetAllPacientes()
    {
        var query = new GetAllPacientesQuery();
        var pacientes = await _mediator.Send(query);
        return Ok(pacientes);
    }
}