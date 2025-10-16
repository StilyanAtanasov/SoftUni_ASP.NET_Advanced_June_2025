namespace CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;

public class UpdateShowtimesViewModel
{
    public Guid CinemaId { get; set; }

    public Guid MovieId { get; set; }

    public ICollection<int> Showtimes { get; set; } = null!;
}