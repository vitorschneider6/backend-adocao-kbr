using Adocao.Data.Mappings;
using Adocao.Models;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Data;

public class AdocaoDevDataContext : DbContext
{
    private readonly string _connectionString = Configuration.ConnectionString;
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Animal> Animais { get; set; }
    public DbSet<Especie> Especies { get; set; }
    public DbSet<Raca> Racas { get; set; }
    public DbSet<Foto> Fotos { get; set; }
    public DbSet<SolicitacaoAdocaoAnimais> Solicitacoes { get; set; }
    public DbSet<RecuperacaoSenha> RecuperacaoSenhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=tcp:adocao-kbr-server.database.windows.net,1433;Initial Catalog=vitor-adocao-kbr;Persist Security Info=False;User ID=adocao-kbr;Password=Milf_addiction0123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdministradorMap());
        modelBuilder.ApplyConfiguration(new RecuperacaoSenhaMap());
        modelBuilder.ApplyConfiguration(new FotoMap());
        modelBuilder.ApplyConfiguration(new AnimalMap());
        modelBuilder.ApplyConfiguration(new EspecieMap());
        modelBuilder.ApplyConfiguration(new RacaMap());
        modelBuilder.ApplyConfiguration(new SolicitacaoMap());
    }
}