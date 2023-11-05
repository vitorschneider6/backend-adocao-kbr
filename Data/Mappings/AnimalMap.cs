using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class AnimalMap : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("Animal");
        builder
            .HasKey(a => a.Id);

        builder
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(a => a.Nome)
            .IsRequired();

        builder
            .Property(a => a.Idade)
            .IsRequired();

        builder
            .Property(a => a.Peso)
            .HasColumnName("Peso");

        builder
            .Property(a => a.Sobre)
            .IsRequired()
            .HasMaxLength(400);

        builder
            .Property(a => a.Ativo)
            .HasDefaultValue(true);

        builder
            .Property(a => a.Local);

        builder
            .Property(a => a.Porte);
        
        builder
            .Property(a => a.Sexo);

        builder
            .HasMany(a => a.Fotos)
            .WithOne(f => f.Animal)
            .HasForeignKey(f => f.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder
            .HasOne(a => a.Raca)
            .WithMany()
            .HasForeignKey(a => a.RacaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(a => a.Solicitacoes)
            .WithOne(s => s.Animal)
            .HasForeignKey(s => s.AnimalId);

        builder.HasOne(a => a.Especie)
            .WithMany(e => e.Animais)
            .HasForeignKey(a => a.EspecieId);
    }
}