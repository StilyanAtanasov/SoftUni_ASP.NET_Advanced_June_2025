namespace CinemaApp.Web.ViewModels.User.Ticket;

public class BuyTicketViewModel
{
    public Guid CinemaId { get; set; }

    public Guid MovieId { get; set; }

    public int Quantity { get; set; }
}