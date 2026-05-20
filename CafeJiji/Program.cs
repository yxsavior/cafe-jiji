using Microsoft.EntityFrameworkCore;
using CafeJiji.Data;

// 1. Carrega o .env antes de QUALQUER outra coisa
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 2. Pega a Connection String direto do arquivo .env
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

// 3. Configura o Banco de Dados (ANTES do builder.Build())
builder.Services.AddDbContext<CafeJijiDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// 4. Outros serviços do container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 5. AGORA SIM, constrói o app depois de configurar tudo
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();