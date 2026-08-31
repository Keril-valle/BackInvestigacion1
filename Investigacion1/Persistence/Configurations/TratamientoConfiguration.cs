using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class TratamientoConfiguration : IEntityTypeConfiguration<Tratamiento>
{
    public void Configure(EntityTypeBuilder<Tratamiento> builder)
    {
        builder.ToTable("Tratamientos");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(t => t.Descripcion).HasMaxLength(1000);
    }
}