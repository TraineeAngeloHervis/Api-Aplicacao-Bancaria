using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ContaMapping : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("Contas");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(x => x.ClienteId)
            .IsRequired();

        builder.Property(x => x.Saldo)
            .IsRequired();

        builder.Property(x => x.DataAbertura)
            .IsRequired();

        builder.Property(x => x.TipoConta)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(c => c.Cliente)
            .WithMany(c => c.Contas)
            .HasForeignKey(c => c.ClienteId);
    }
}