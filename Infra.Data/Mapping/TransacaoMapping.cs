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
        
        builder.Property(x => x.DataTransacao)
            .IsRequired();

        builder.Property(x => x.Valor)
            .IsRequired();

        builder.HasOne(x => x.Conta)
            .WithMany()
            .HasForeignKey(x => x.ContaOrigemId);
    }
}