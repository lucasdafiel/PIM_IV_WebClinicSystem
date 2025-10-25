using Microsoft.AspNetCore.Authentication.JwtBearer; // Adicionar este using
using Microsoft.IdentityModel.Tokens; // Adicionar este using
using System.Text; // Adicionar este using
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebClinicSystem.Application.Features.Pacientes.Commands;
using WebClinicSystem.Domain.Interfaces;
using WebClinicSystem.Infrastructure.Auth;
using WebClinicSystem.Infrastructure.Persistence;
using WebClinicSystem.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- In�cio da Se��o de Configura��o ---

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebClinicDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CadastrarPacienteCommand>());

// --- ADICIONAR CONFIGURA��O DE AUTENTICA��O JWT ---
// Este bloco ensina a API a validar o token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// --- Fim da Configura��o de Autentica��o ---

builder.Services.AddControllers();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7155")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();

// Configura��o do Swagger (j� estava correta)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebClinicSystem.Api", Version = "v1" });

    // Defini��o de seguran�a para o Swagger: informa que a API usa Bearer Token (JWT).
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Adiciona o requisito de seguran�a a todas as opera��es,
    // fazendo com que o Swagger envie o token em cada requisi��o.
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);    


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();