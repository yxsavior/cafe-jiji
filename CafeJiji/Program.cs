using CafeJiji.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CafeJiji.Services;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

// 1. Carrega o .env para a Connection String
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 2. Configuração do Banco de Dados
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
builder.Services.AddDbContext<CafeJijiDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// 3. Controllers com suporte a ENUM (mostra "Aberto" em vez de 0)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 4. Configuração do Swagger com Suporte a JWT (Cadeado para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Café Jiji API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// 5. Configuração de Autenticação JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "chave-reserva-com-mais-de-32-caracteres");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

// 6. Configuração de CORS (Permitir que o Front-end acesse a API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// --- REGISTRO DE SERVICES ---
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// 7. Popular o banco automaticamente (Seed)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CafeJijiDbContext>();
    // context.Database.Migrate(); // Opcional: aplica migrations automaticamente
    CafeJijiDbSeeder.Seed(context);
}

// 8. Pipeline de Execução (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CafeJiji v1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Ativa o CORS

app.UseAuthentication(); // OBRIGATÓRIO vir antes do Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();