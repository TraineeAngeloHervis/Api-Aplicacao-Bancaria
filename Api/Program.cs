using Infra.Data;
using Api.Configuration;
using Api.Middleware;
using Domain.Interfaces;
using Domain.Services;
using Domain.Validators;
using Infra.Data.Repository;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ContaValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AcessoTotal",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IClienteValidator, ClienteValidator>();
builder.Services.AddScoped<IContaValidator, ContaValidator>();


builder.Services.AddConfiguracaoAutoMapper();

builder.Services.AddDbContext<AppDbContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.TratarExcecao();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AcessoTotal");
app.UseAuthorization();

app.MapControllers();

app.Run();