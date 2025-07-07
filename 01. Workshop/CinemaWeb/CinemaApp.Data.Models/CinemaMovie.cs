using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CinemaApp.Data.Common.EntityConstraints.CinemaMovie;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Models;

[PrimaryKey(nameof(MovieId), nameof(CinemaId))]
public class CinemaMovie
{
    [Required]
    [ForeignKey(nameof(Movie))]
    public Guid MovieId { get; set; }

    [Required]
    public Movie Movie { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Cinema))]
    public Guid CinemaId { get; set; }

    [Required]
    public Cinema Cinema { get; set; } = null!;

    [Required]
    public int AvailableTickets{ get; set; }

    public bool IsDeleted { get; set; }

    [Column(TypeName = ShowtimeDataType)]
    public string Showtime { get; set; } = null!;
}