using Microsoft.EntityFrameworkCore;
using WebClinicSystem.Application.Feature.Pacientes.Commands; // Necess�rio para o MediatR encontrar os Handlers
using WebClinicSystem.Domain.Interfaces;
using WebClinicSystem.Infrastructure.Persistence;
using WebClinicSystem.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- In�cio da Se��o de Configura��o ---

// 1. Adicionar a string de conex�o e registrar o DbContext.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebClinicDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Registrar as interfaces e suas implementa��es (Inje��o de Depend�ncia).
// Sempre que algu�m pedir um 'IPacienteRepository', o sistema entregar� um 'PacienteRepository'.
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// 3. Registrar o MediatR.
// O comando 'AddMediatR' vai procurar por todos os Comandos e Handlers no projeto Application.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CadastrarPacienteCommand>());

// --- Fim da Se��o de Configura��o ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();