namespace CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;

public class MovieShowtimesViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public ICollection<int> Showtimes { get; set; } = new HashSet<int>();
}