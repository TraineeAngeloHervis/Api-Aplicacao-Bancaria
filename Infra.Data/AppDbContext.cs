using Domain.Clientes.Entities;
using Domain.Contas.Entities;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : DbContext
{
    //Configuração do banco de dados para MYSQL
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
        optionsBuilder.UseMySql("Server=localhost;Database=aplicacao_bancaria;Uid=root;Pwd=BancoApi007;", serverVersion);
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conta> Contas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new ClienteMapping())
            .ApplyConfiguration(new ContaMapping());
    }
}