using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static CinemaApp.Data.Common.EntityConstraints.Ticket;

namespace CinemaApp.Data.Models;

public class Ticket
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = PriceDataType)]
    public decimal Price { get; set; }

    [Required]
    [ForeignKey(nameof(Cinema))]
    public Guid CinemaId { get; set; }

    [Required]
    public Cinema Cinema { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Movie))]
    public Guid MovieId { get; set; }

    [Required]
    public Movie Movie { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    [Required]
    public IdentityUser User { get; set; } = null!;
}