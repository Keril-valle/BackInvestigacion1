using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("Citas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("pendiente");
        builder.Property(c => c.Notas).HasMaxLength(1000);

        builder.HasOne(c => c.Paciente)
            .WithMany(p => p.Citas)
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Dermatologo)
            .WithMany(d => d.Citas)
            .HasForeignKey(c => c.DermatologoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Servicio)
            .WithMany(s => s.Citas)
            .HasForeignKey(c => c.ServicioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}