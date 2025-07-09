using System.ComponentModel.DataAnnotations;
using static CinemaApp.Data.Common.EntityConstraints.Cinema;

namespace CinemaApp.Data.Models;

public class Cinema
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(LocationMaxLength)]
    public string Location { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<CinemaMovie> CinemasMovies { get; set; } = new HashSet<CinemaMovie>();

    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}