using Infra.Data;
using Microsoft.OpenApi.Models;
using Api.Configuration;
using Domain.Clientes.Interfaces;
using Domain.Clientes.Services;
using Domain.Contas.Interfaces;
using Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

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


builder.Services.AddConfiguracaoAutoMapper();

builder.Services.AddDbContext<AppDbContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AcessoTotal");
app.UseAuthorization();

app.MapControllers();

app.Run();