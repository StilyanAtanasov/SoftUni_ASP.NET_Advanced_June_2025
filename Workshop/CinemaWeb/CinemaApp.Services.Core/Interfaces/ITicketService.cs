using CinemaApp.Web.ViewModels.Ticket;

namespace CinemaApp.Services.Core.Interfaces;

public interface ITicketService
{
    Task<ICollection<UserTicketViewModel>> GetUserTicketsReadonlyAsync(string userId);
}
