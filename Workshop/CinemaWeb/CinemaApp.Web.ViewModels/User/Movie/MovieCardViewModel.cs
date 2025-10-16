namespace CinemaApp.Web.ViewModels.User.Movie;

public class MovieCardViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string ReleaseDate { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public bool IsInWatchlist { get; set; }

    public string? ImageUrl { get; set; }
}