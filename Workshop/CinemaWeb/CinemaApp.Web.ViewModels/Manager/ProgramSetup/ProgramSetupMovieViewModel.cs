namespace CinemaApp.Web.ViewModels.Manager.ProgramSetup;

public class ProgramSetupMovieViewModel
{
    public Guid MovieId { get; set; }

    public string? PosterUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Duration  { get; set; }

    public bool IsIncluded { get; set; }
}
