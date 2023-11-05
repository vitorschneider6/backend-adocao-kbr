using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class RecuperacaoSenhaMap : IEntityTypeConfiguration<RecuperacaoSenha>
{
    public void Configure(EntityTypeBuilder<RecuperacaoSenha> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder
            .Property(r => r.Id)
            .HasDefaultValue(Guid.NewGuid());


        builder
            .Property(r => r.Expiration)
            .HasDefaultValue(DateTime.UtcNow.AddDays(10));

        builder
            .Property(r => r.AdministradorId);

        builder
            .HasOne(r => r.Administrador)
            .WithOne(a => a.Recuperacao)
            .HasForeignKey<RecuperacaoSenha>(r => r.AdministradorId);

    }
}