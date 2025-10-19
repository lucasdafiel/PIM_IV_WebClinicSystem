using Microsoft.EntityFrameworkCore;
using WebClinicSystem.Application.Feature.Pacientes.Commands; // Necessário para o MediatR encontrar os Handlers
using WebClinicSystem.Domain.Interfaces;
using WebClinicSystem.Infrastructure.Persistence;
using WebClinicSystem.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Início da Seção de Configuração ---

// 1. Adicionar a string de conexão e registrar o DbContext.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebClinicDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Registrar as interfaces e suas implementações (Injeção de Dependência).
// Sempre que alguém pedir um 'IPacienteRepository', o sistema entregará um 'PacienteRepository'.
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// 3. Registrar o MediatR.
// O comando 'AddMediatR' vai procurar por todos os Comandos e Handlers no projeto Application.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CadastrarPacienteCommand>());

// --- Fim da Seção de Configuração ---

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