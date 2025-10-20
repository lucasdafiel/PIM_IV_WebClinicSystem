using Microsoft.EntityFrameworkCore;
using WebClinicSystem.Application.Features.Pacientes.Commands;
using WebClinicSystem.Domain.Interfaces;
using WebClinicSystem.Infrastructure.Persistence;
using WebClinicSystem.Infrastructure.Persistence.Repositories;
using WebClinicSystem.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// --- In�cio da Se��o de Configura��o ---

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebClinicDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthService, AuthService>();

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