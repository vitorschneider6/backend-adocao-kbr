using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class RacaMap : IEntityTypeConfiguration<Raca>
{
    public void Configure(EntityTypeBuilder<Raca> builder)
    {
        builder.ToTable("Raca");
        builder.HasIndex(
            r => r.Nome).IsUnique();
        
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(r => r.Nome)
            .IsRequired();

        builder
            .Property(r => r.Descricao)
            .IsRequired()
            .HasMaxLength(400);

        builder
            .HasOne(r => r.Especie)
            .WithMany(e => e.Racas)
            .HasForeignKey(r => r.EspecieId)
            .IsRequired();

        builder.HasMany(r => r.Animais)
            .WithOne(a => a.Raca)
            .HasForeignKey(a => a.RacaId);
    }
}