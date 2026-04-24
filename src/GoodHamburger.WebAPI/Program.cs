using FluentValidation;
using GoodHamburger.Application.DependencyInjection;
using GoodHamburger.Infra.Data;
using GoodHamburger.Infra.DependencyInjection;
using GoodHamburger.Shared.Validators;
using GoodHamburger.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidatorsFromAssemblyContaining<PedidoRequestValidator>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

app.UseCors("DevCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    for (int i = 1; i <= 5; i++)
    {
        try
        {
            logger.LogInformation("Tentativa {Tentativa} de aplicar as migrations...", i);
            var context = services.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
            logger.LogInformation("Banco de Dados criado e populado com sucesso!");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning("O banco de dados ainda não está pronto. Aguardando 5 segundos...");
            Thread.Sleep(5000);
        }
    }
}

app.Run();