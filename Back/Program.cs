using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Royal_Games.Applications.Autenticacao;
using Royal_Games.Applications.Services;
using Royal_Games.Contexts;
using Royal_Games.Interfaces;
using Royal_Games.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });
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

// Chamar nossa conexão com o banco aqui na program.
builder.Services.AddDbContext<Royal_GamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Autenticação (JWT)
builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

// Classificação Indicativa
builder.Services.AddScoped<IClassificacaoIndicativaRepository, ClassificacaoIndicativaRepository>();
builder.Services.AddScoped<ClassificacaoIndicativaService>();

// Gênero
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<GeneroService>();

// Jogo
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<JogoService>();

// Log Alteração
builder.Services.AddScoped<ILogAlteracaoJogoRepository, LogAlteracaoJogoRepository>();
builder.Services.AddScoped<LogAlteracaoJogoService>();

// Plataforma
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddScoped<PlataformaService>();

// Promoção
builder.Services.AddScoped<IPromocaoRepository, PromocaoRepository>();
builder.Services.AddScoped<PromocaoService>();

// Usuário
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// Configura o sistema de autenticação da aplicação.
// Aqui estamos dizendo que o tipo de autenticação padrão será Jwt Bearer.
// Ou seja: a API vai esperar receber um Token Jwt nas requisições.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando Jwt.
    .AddJwtBearer(options =>
    {
        // Lê a chave secreta definIda no appsettings.json.
        // Essa chave é usada para ASSINAR o token quando ele é gerado
        // e também para ValidAR se o token recebido é verdadeiro.
        var chave = builder.Configuration["Jwt:Key"]!;

        // Quem emitiu o token (ex: nome da sua aplicação).
        // Serve para evitar aceitar tokens de outro sistema.
        var issuer = builder.Configuration["Jwt:Issuer"]!;

        // Para quem o token foi criado (normalmente o frontend ou a própria API).
        // Também ajuda a garantir que o token pertence ao seu sistema.
        var audience = builder.Configuration["Jwt:Audience"]!;

        // Define as regras que serão usadas para Validar o token recebido.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verifica se o emissor do token é válIdo
            // (se bate com o issuer configurado).
            ValidateIssuer = true,

            // Verifica se o destinatário do token é válIdo
            // (se bate com o audience configurado).
            ValidateAudience = true,

            // Verifica se o token ainda está dentro do prazo de Validade.
            // Se já expirou, a requisição será negada.
            ValidateLifetime = true,

            // Verifica se a assinatura do token é válIda.
            // Isso garante que o token não foi alterado.
            ValidateIssuerSigningKey = true,

            // Define qual emissor é consIderado válIdo.
            ValidIssuer = issuer,

            // Define qual audience é consIderado válIdo.
            ValidAudience = audience,

            // Define qual chave será usada para Validar a assinatura do token.
            // A mesma chave usada na geração do Jwt deve estar aqui.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            )
        };
    });

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Adicionar CORS
app.UseCors("CorsPolicy");

app.MapControllers();
app.Run();