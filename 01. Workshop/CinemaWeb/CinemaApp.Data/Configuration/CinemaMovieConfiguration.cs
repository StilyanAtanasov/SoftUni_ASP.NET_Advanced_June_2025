using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Data.Common.EntityConstraints.CinemaMovie;

namespace CinemaApp.Data.Configuration;

public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
{
    public void Configure(EntityTypeBuilder<CinemaMovie> builder)
    {
        builder
            .Property(cm => cm.IsDeleted)
            .HasDefaultValue(IsDeletedDefaultValue);

        builder
            .Property(cm => cm.Showtime)
            .HasMaxLength(ShowtimeMaxLength)
            .HasColumnType(ShowtimeDataType)
            .HasDefaultValue(ShowtimeDefaultValue);

        builder
            .HasOne(cm => cm.Cinema)
            .WithMany(c => c.CinemasMovies)
            .HasForeignKey(cm => cm.CinemaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(cm => cm.Movie)
            .WithMany(m => m.CinemasMovies)
            .HasForeignKey(cm => cm.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
