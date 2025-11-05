using System;
using WebClinicSystem.Web.Services;
using WebClinicSystem.Web.HttpHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Registra HttpClientFactory e um cliente nomeado para consumir a API WebClinicSystem
builder.Services.AddHttpClient();

// Registra o handler que injeta o token nas requisições
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<IPacienteApiService, PacienteApiService>();
builder.Services.AddScoped<IProfissionalApiService, ProfissionalApiService>();
builder.Services.AddScoped<IConsultaApiService, ConsultaApiService>();
builder.Services.AddScoped<IUsuarioApiService, UsuarioApiService>();
builder.Services.AddScoped<IProntuarioApiService, ProntuarioApiService>();

builder.Services.AddHttpClient("WebClinicSystemApi", client =>
{
    // Lê a URL da API a partir da configuração (appsettings.json ou variável de ambiente).
    // Se não estiver configurada, usa o valor padrão do launchSettings da API (https://localhost:7106).
    var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7106";
    client.BaseAddress = new Uri(apiUrl);
})
// Adiciona o handler para que o token JWT da sessão seja incluído automaticamente
.AddHttpMessageHandler<AuthTokenHandler>(); 

// Registra o serviço que consome a API de Pacientes
builder.Services.AddScoped<IPacienteApiService, PacienteApiService>();

// Registra serviço de autenticação via API e IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();

// Adiciona autenticação baseada em Cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        // Caminho para a página de login
        options.LoginPath = "/Login";
        // Caminho para o logout (página que lida com saída)
        options.LogoutPath = "/Login/Logout";
        // Caminho quando o acesso é negado
        options.AccessDeniedPath = "/AccessDenied";
    });

// Adiciona serviços de sessão em memória
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Timeout de inatividade da sessão
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Habilita a sessão antes da autenticação/autorization
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
