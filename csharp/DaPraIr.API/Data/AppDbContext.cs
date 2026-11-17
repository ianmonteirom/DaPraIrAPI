using DaPraIr.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DaPraIr.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Lugar> Lugares => Set<Lugar>();
    public DbSet<Ficha> Fichas => Set<Ficha>();
    public DbSet<Relato> Relatos => Set<Relato>();
    public DbSet<Aviso> Avisos => Set<Aviso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lugar>(e =>
        {
            e.HasKey(l => l.Id);
            e.Property(l => l.Nome).IsRequired().HasMaxLength(200);
            e.Property(l => l.Tipo).IsRequired().HasMaxLength(50);
            e.Property(l => l.Bairro).IsRequired().HasMaxLength(100);
            e.HasOne(l => l.Ficha).WithOne(f => f.Lugar).HasForeignKey<Ficha>(f => f.LugarId);
            e.HasMany(l => l.Relatos).WithOne(r => r.Lugar).HasForeignKey(r => r.LugarId);
            e.HasMany(l => l.Avisos).WithOne(a => a.Lugar).HasForeignKey(a => a.LugarId);
        });

        modelBuilder.Entity<Ficha>(e =>
        {
            e.HasKey(f => f.Id);
            e.Property(f => f.NivelBarulho).HasMaxLength(10);
        });
    }
}
