using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CinemaApp.Data.Common.EntityConstraints.Movie;

namespace CinemaApp.Data.Models;

[Comment("movie in the system")]
public class Movie
{
	[Comment("Movie Identifier")]
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Comment("Movie Title")]
	[Required(ErrorMessage = "Title is required!")]
	[StringLength(TitleMaxLength, ErrorMessage = "Title cannot exceed 100 characters!")]
	public string Title { get; set; } = null!;

	[Comment("Movie Genre")]
	[Required(ErrorMessage = "Genre is required!")]
	[StringLength(GenreMaxLength, ErrorMessage = "Genre cannot exceed 50 characters!")]
	public string Genre { get; set; } = null!;

	[Comment("Movie Release Date")]
	[Required(ErrorMessage = "Release date is required!")]
	public DateTime ReleaseDate { get; set; }

	[Comment("Movie Director")]
	[Required(ErrorMessage = "Director name is required!")]
	[StringLength(DirectorNameMaxLength, ErrorMessage = "Director name cannot exceed 100 characters!")]
	public string Director { get; set; } = null!;

	[Comment("Movie Duration")]
	[Required(ErrorMessage = "Duration is required!")]
	[Range(DurationMin, DurationMax, ErrorMessage = "Duration must be between 1 and 300 minutes")]
	public int Duration { get; set; }

	[Comment("Movie Description")]
	[Required(ErrorMessage = "Description is required!")]
	[StringLength(DescriptionMaxLength, ErrorMessage = "Description cannot exceed 1000 characters!")]
	public string Description { get; set; } = null!;

	[Comment("Movie Image URL")]
	public string? ImageUrl { get; set; }

	public bool IsDeleted { get; set; } = false;

	[Comment("Collection of cinemas showing this movie")]
	public ICollection<CinemaMovie> CinemasMovies { get; set; } = new HashSet<CinemaMovie>();

    [Comment("Collection of tickets for this movie")]
    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}