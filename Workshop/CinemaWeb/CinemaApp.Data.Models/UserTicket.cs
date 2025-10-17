using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Data.Models;

[PrimaryKey(nameof(UserId), nameof(TicketId))]
public class UserTicket
{
    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    [Required]
    public ApplicationUser User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Ticket))]
    public Guid TicketId { get; set; }

    [Required]
    public Ticket Ticket { get; set; } = null!;
}