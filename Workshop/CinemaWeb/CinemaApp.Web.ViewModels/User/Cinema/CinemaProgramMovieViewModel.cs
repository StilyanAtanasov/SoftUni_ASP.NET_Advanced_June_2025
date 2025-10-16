namespace CinemaApp.Web.ViewModels.User.Cinema;

public class CinemaProgramMovieViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string? ImageUrl { get; set; } = null!;
}
