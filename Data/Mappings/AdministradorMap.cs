using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class AdministradorMap : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder
            .ToTable("Administrador")
            .HasIndex(a => a.Email)
            .IsUnique();

        builder
            .HasKey(a => a.Id);

        builder
            .Property(a => a.Id)
            .HasDefaultValue(Guid.NewGuid());

        builder
            .Property(a => a.Nome)
            .IsRequired();

        builder
            .Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(120);

        builder
            .Property(a => a.Senha)
            .IsRequired();
    }
}