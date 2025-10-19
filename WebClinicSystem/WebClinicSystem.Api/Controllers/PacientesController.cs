using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Application.Features.Pacientes.Commands;
using WebClinicSystem.Application.Features.Pacientes.DTOs; // Importando nosso DTO

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
    public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteDto createPacienteDto)
    {
        // 1. Mapeia os dados do DTO (que veio da requisição) para o nosso Comando.
        var command = new CadastrarPacienteCommand(
            createPacienteDto.NomeCompleto,
            createPacienteDto.Cpf,
            createPacienteDto.DataNascimento,
            createPacienteDto.TelefoneContato
        );

        // 2. Envia o comando para o MediatR, que vai encontrar e executar o Handler correspondente.
        var pacienteId = await _mediator.Send(command);

        // 3. Retorna uma resposta HTTP 201 Created.
        // Isso indica que o recurso foi criado com sucesso e informa a URL para acessá-lo.
        // (Ainda vamos criar o endpoint 'GetPacienteById' para que esta URL funcione).
        return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteId }, new { id = pacienteId });
    }

    // --- Endpoint para Buscar um Paciente por ID (GET) ---
    // Vamos adicionar um endpoint de busca para que a URL do CreatedAtAction funcione.
    [HttpGet("{id:int}")] // A rota será "api/pacientes/{id}"
    public async Task<IActionResult> GetPacienteById(int id)
    {
        // (A lógica para buscar o paciente será implementada na próxima tarefa do Trello).
        // Por enquanto, apenas retornamos um OK para o endpoint existir.
        return Ok($"Endpoint para buscar paciente com ID {id} - a ser implementado.");
    }
}