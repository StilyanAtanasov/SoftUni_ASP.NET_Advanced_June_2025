namespace CinemaApp.Web.ViewModels.Manager.TicketManagement;

public class TicketManagementIndexViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public bool HasMovies { get; set; }
}