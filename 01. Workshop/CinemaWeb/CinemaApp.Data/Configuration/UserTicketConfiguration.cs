using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration;

public class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
{
    public void Configure(EntityTypeBuilder<UserTicket> builder)
    {
        builder
            .HasOne(ut => ut.Ticket)
            .WithMany()
            .HasForeignKey(ut => ut.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(ut => ut.User)
            .WithMany()
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}