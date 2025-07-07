using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Models;

[PrimaryKey(nameof(UserId), nameof(TicketId))]
public class UserTicket
{
    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    [Required]
    public IdentityUser User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Ticket))]
    public Guid TicketId { get; set; }

    [Required]
    public Ticket Ticket { get; set; } = null!;
}