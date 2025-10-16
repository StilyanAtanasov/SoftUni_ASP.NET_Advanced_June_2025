using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.Manager.TicketManagement;

public class UpdateCinemaMovieTicketsCountViewModel
{
    public Guid MovieId { get; set; }

    public Guid CinemaId { get; set; }

    [Range(0, 1000)]
    public int AvailableTickets { get; set; }
}