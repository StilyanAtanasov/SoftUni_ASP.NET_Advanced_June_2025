using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models;

public class UserMovie
{
    public string UserId { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;

    public Guid MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}