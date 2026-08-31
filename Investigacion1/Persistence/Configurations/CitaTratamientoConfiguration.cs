using Investigacion1.Features.Clinica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investigacion1.Persistence.Configurations;

public class CitaTratamientoConfiguration : IEntityTypeConfiguration<CitaTratamiento>
{
    public void Configure(EntityTypeBuilder<CitaTratamiento> builder)
    {
        builder.ToTable("CitaTratamientos");
        builder.HasKey(ct => ct.Id);

        builder.HasIndex(ct => new { ct.CitaId, ct.TratamientoId }).IsUnique();

        builder.HasOne(ct => ct.Cita)
            .WithMany(c => c.CitaTratamientos)
            .HasForeignKey(ct => ct.CitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ct => ct.Tratamiento)
            .WithMany(t => t.CitaTratamientos)
            .HasForeignKey(ct => ct.TratamientoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}