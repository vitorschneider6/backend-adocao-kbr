using Adocao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adocao.Data.Mappings;

public class FotoMap : IEntityTypeConfiguration<Foto>
{
    public void Configure(EntityTypeBuilder<Foto> builder)
    {
        builder.ToTable("Foto");
        
        builder.HasKey(f => f.FotoId);

        builder
            .Property(f => f.FotoId)
            .ValueGeneratedOnAdd();

        builder.HasOne(f => f.Animal)
            .WithMany(a => a.Fotos)
            .HasForeignKey(a => a.FotoId);
    }
}
    