namespace CinemaApp.Web.ViewModels.Ticket;

public class UserTicketViewModel
{
    public Guid Id { get; set; }

    public string MovieTitle { get; set; } = null!;

    public string CinemaName{ get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; } = null!;

    public int TicketCount{ get; set; }
}