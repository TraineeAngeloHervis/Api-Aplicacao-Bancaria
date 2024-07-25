using Api.Configuration;
using Domain.Interfaces;
using Domain.Services;
using Domain.Validators;
using FluentValidation;
using Infra.Data;
using Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ContaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferenciaValidator>();

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
builder.Services.AddScoped<IClienteValidator, ClienteValidator>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IContaValidator, ContaValidator>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ITransferenciaValidator, TransferenciaValidator>();


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