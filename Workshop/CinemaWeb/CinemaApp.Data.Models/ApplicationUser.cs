using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models;

public class ApplicationUser : IdentityUser
{
    public bool IsDeleted { get; set; }

    public ICollection<UserMovie> UserMovies { get; set; } = new HashSet<UserMovie>();

    public ICollection<UserTicket> UserTickets { get; set; } = new HashSet<UserTicket>();
}