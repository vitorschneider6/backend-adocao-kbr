using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class SolicitacaoMap : IEntityTypeConfiguration<SolicitacaoAdocaoAnimais>
{
    public void Configure(EntityTypeBuilder<SolicitacaoAdocaoAnimais> builder)
    {
        builder
            .ToTable("Solicitacao");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.NomeSolicitante)
            .IsRequired();

        builder
            .Property(s => s.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder
            .Property(s => s.Email)
            .IsRequired();

        builder
            .Property(s => s.Celular)
            .IsRequired();

        builder
            .Property(s => s.DataNascimento)
            .IsRequired();

        builder
            .Property(s => s.DataSolicitacao)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETDATE()");

        builder
            .HasOne(s => s.Animal)
            .WithMany(a => a.Solicitacoes)
            .HasForeignKey(s => s.AnimalId)
            .IsRequired();
    }
}