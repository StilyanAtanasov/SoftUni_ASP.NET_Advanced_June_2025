using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Data.Common.EntityConstraints.Cinema;

namespace CinemaApp.Data.Configuration;

public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder
            .Property(c => c.IsDeleted)
            .HasDefaultValue(IsDeletedDefaultValue);
    }
}