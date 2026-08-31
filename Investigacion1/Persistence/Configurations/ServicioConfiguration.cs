using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.ToTable("Servicios");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(s => s.Precio).HasPrecision(10, 2);
        builder.Property(s => s.Activo).HasDefaultValue(true);

        builder.HasMany(s => s.Citas)
            .WithOne(c => c.Servicio)
            .HasForeignKey(c => c.ServicioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}