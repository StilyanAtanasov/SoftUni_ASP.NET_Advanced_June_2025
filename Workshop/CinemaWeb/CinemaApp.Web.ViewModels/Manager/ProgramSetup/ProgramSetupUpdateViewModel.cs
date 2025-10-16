namespace CinemaApp.Web.ViewModels.Manager.ProgramSetup;

public class ProgramSetupUpdateViewModel
{
    public Guid CinemaId { get; set; }

    public ICollection<ProgramSetupMovieViewModel> Movies { get; set; } = new HashSet<ProgramSetupMovieViewModel>();
}
