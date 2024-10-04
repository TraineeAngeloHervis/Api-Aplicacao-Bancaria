using System.Data;
using Domain.Entities;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Infra.Data;

public class AppDbContext : DbContext
{
    private const string ConnectionString = "Server=localhost;Database=aplicacao_bancaria;Uid=root;Pwd=BancoApi007;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
        optionsBuilder.UseMySql(ConnectionString, serverVersion);
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new ClienteMapping())
            .ApplyConfiguration(new ContaMapping())
            .ApplyConfiguration(new TransacaoMapping());
    }

    public static IDbConnection GetDapperContext()
    {
        return new MySqlConnection(ConnectionString);
    }
}