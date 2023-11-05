using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class EspecieMap : IEntityTypeConfiguration<Especie>
{
    public void Configure(EntityTypeBuilder<Especie> builder)
    {
        builder.ToTable("Especie");
        builder.HasIndex(e => e.Nome).IsUnique();
        
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(e => e.Nome)
            .IsRequired();

        builder
            .HasMany(e => e.Racas)
            .WithOne(r => r.Especie)
            .HasForeignKey(r => r.EspecieId);

    }
}