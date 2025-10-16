namespace CinemaApp.Web.ViewModels.User.Cinema;

public class CinemaProgramViewModel
{
    public Guid CinemaId { get; set; }

    public string CinemaName { get; set; } = null!;

    public ICollection<CinemaProgramMovieViewModel> Movies { get; set; } = null!;
}
