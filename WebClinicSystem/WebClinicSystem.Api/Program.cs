// Importações necessárias para o funcionamento da aplicação.
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebClinicSystem.Application.Features.Auth.Commands;
using WebClinicSystem.Domain.Interfaces;
using WebClinicSystem.Infrastructure.Auth;
using WebClinicSystem.Infrastructure.Persistence;
using WebClinicSystem.Infrastructure.Persistence.Repositories;

// Cria o construtor da aplicação web.
var builder = WebApplication.CreateBuilder(args);

// --- INÍCIO DA SOLUÇÃO ---

// Define um nome para a nossa política de CORS para facilitar a referência.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Adiciona o serviço de CORS (Cross-Origin Resource Sharing).
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Lê a LISTA de URLs permitidas do arquivo de configuração.
                          // O método GetSection("AllowedOrigins").Get<string[]>() busca a seção
                          // e a converte em um array de strings.
                          var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

                          // Valida se a configuração foi encontrada para evitar erros.
                          if (allowedOrigins != null && allowedOrigins.Length > 0)
                          {
                              // Permite que TODAS as origens listadas no appsettings
                              // se comuniquem com esta API.
                              policy.WithOrigins(allowedOrigins)
                                    .AllowAnyHeader()  // Permite qualquer cabeçalho na requisição (ex: Content-Type).
                                    .AllowAnyMethod(); // Permite qualquer método HTTP (GET, POST, PUT, DELETE).
                          }
                      });
}); 

// --- FIM DA SOLUÇÃO ---

// Adiciona os serviços de controllers para a aplicação.
builder.Services.AddControllers();

// Configura o Entity Framework Core para usar o SQL Server.
// A string de conexão é lida do arquivo appsettings.json.
builder.Services.AddDbContext<WebClinicDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adiciona os serviços do MediatR para implementar o padrão CQRS.
// Ele vai escanear o assembly da aplicação em busca de handlers de comandos e queries.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommand>());

// Injeção de Dependência: Registra as interfaces e suas implementações concretas.
// Isso permite que a aplicação seja mais desacoplada e testável.
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configura o Swagger/OpenAPI para documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração da autenticação JWT (JSON Web Token).
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // Configurações de validação do token, lidas do appsettings.json.
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Constrói a aplicação.
var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- INÍCIO DA SOLUÇÃO ---

// Manda a aplicação USAR a política de CORS que definimos acima.
// Isso deve vir antes de UseAuthorization e MapControllers.
app.UseCors(MyAllowSpecificOrigins);

// --- FIM DA SOLUÇÃO ---

app.UseAuthentication(); // Adiciona o middleware de autenticação.
app.UseAuthorization();  // Adiciona o middleware de autorização.

app.MapControllers(); // Mapeia os controllers para as rotas.

app.Run(); // Executa a aplicação.