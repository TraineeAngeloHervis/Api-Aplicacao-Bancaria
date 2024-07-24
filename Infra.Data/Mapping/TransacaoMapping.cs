using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transações");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(x => x.ContaOrigemId)
            .IsRequired();

        builder.Property(x => x.ContaDestinoId)
            .IsRequired();

        builder.Property(x => x.Valor)
            .IsRequired();

        builder.Property(x => x.DataTransacao)
            .IsRequired();

        builder.Property(x => x.TipoTransacao)
            .IsRequired();

        builder.HasOne(c => c.ContaOrigem)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(x => x.ContaOrigemId);

        builder.HasOne(x => x.ContaDestino)
            .WithMany()
            .HasForeignKey(x => x.ContaDestinoId);
    }
}