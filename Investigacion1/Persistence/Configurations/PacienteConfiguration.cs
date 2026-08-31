using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Telefono).HasMaxLength(30);
        builder.Property(p => p.FechaNacimiento).HasColumnType("date");

        builder.HasOne(p => p.Usuario)
            .WithOne(u => u.Paciente)
            .HasForeignKey<Paciente>(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.UsuarioId).IsUnique();

        builder.HasMany(p => p.Citas)
            .WithOne(c => c.Paciente)
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}