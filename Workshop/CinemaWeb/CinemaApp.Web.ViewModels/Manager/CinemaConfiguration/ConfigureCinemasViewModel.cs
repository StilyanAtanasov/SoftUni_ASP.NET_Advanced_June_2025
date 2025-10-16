namespace CinemaApp.Web.ViewModels.Manager.CinemaConfiguration;

public class ConfigureCinemasViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;
}
