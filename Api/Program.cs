using System.Text.Json.Serialization;
using Api.Configuration;
using Domain.Interfaces.Clientes;
using Domain.Interfaces.Contas;
using Domain.Interfaces.Transacoes;
using Domain.Services;
using Domain.Validators;
using FluentValidation;
using Infra.Data;
using Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ContaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferenciaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DepositoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SaqueValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AcessoTotal",
        policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteValidator, ClienteValidator>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IContaValidator, ContaValidator>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<ITransferenciaValidator, TransferenciaValidator>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IDepositoService, DepositoService>();
builder.Services.AddScoped<IDepositoValidator, DepositoValidator>();
builder.Services.AddScoped<ISaqueService, SaqueService>();
builder.Services.AddScoped<ISaqueValidator, SaqueValidator>();



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

await app.RunAsync();