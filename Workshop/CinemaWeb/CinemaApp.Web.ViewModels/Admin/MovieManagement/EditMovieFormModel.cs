namespace CinemaApp.Web.ViewModels.Admin.MovieManagement;

using System.ComponentModel.DataAnnotations;
using static CinemaApp.Data.Common.EntityConstraints.Movie;

public class EditMovieFormModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(GenreMaxLength, MinimumLength = GenreMinLength)]
    public string Genre { get; set; } = null!;

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [StringLength(DirectorNameMaxLength, MinimumLength = DirectorNameMinLength)]
    public string Director { get; set; } = null!;

    [Required]
    public int Duration { get; set; }

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [StringLength(ImageUrlMaxLength)]
    public string? ImageUrl { get; set; }
}
