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
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ContaOrigemId)
            .IsRequired();
        
        builder.Property(x => x.ContaDestinoId)
            .IsRequired(false);
        
        builder.Property(x => x.DataTransacao)
            .IsRequired();

        builder.Property(x => x.Valor)
            .IsRequired();
        
        builder.Property(x => x.TipoTransacao)
            .IsRequired();

        builder.HasOne(x => x.ContaOrigem)
            .WithMany()
            .HasForeignKey(x => x.ContaOrigemId);
    }
}