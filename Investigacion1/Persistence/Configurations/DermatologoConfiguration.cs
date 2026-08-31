using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class DermatologoConfiguration : IEntityTypeConfiguration<Dermatologo>
{
    public void Configure(EntityTypeBuilder<Dermatologo> builder)
    {
        builder.ToTable("Dermatologos");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(d => d.Especialidad).HasMaxLength(150);
        builder.Property(d => d.NumeroLicencia).IsRequired().HasMaxLength(50);
        builder.HasIndex(d => d.NumeroLicencia).IsUnique();

        builder.HasOne(d => d.Usuario)
            .WithOne(u => u.Dermatologo)
            .HasForeignKey<Dermatologo>(d => d.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.UsuarioId).IsUnique();

        builder.HasMany(d => d.Citas)
            .WithOne(c => c.Dermatologo)
            .HasForeignKey(c => c.DermatologoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}