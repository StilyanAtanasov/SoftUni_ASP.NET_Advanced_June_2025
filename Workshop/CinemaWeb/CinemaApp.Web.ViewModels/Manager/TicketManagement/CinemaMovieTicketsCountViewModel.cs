namespace CinemaApp.Web.ViewModels.Manager.TicketManagement;

public class CinemaMovieTicketsCountViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Guid CinemaId { get; set; }

    public int AvailableTickets { get; set; }
}