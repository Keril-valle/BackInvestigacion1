using Investigacion1.Features.Clinica;
using Investigacion1.Features.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Paciente> Pacientes => Set<Paciente>();

    public DbSet<Dermatologo> Dermatologos => Set<Dermatologo>();

    public DbSet<Servicio> Servicios => Set<Servicio>();

    public DbSet<Cita> Citas => Set<Cita>();

    public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();

    public DbSet<CitaTratamiento> CitaTratamientos => Set<CitaTratamiento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.Usuario)
            .WithMany()
            .HasForeignKey(rt => rt.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}