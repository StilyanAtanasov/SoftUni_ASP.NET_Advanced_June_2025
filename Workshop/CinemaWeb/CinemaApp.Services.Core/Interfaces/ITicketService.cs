using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Ticket;
using CinemaApp.Web.ViewModels.User.Ticket;

namespace CinemaApp.Services.Core.Interfaces;

public interface ITicketService
{
    Task<ICollection<UserTicketViewModel>> GetUserTicketsReadonlyAsync(string userId);

    Task<ServiceResult> BuyTicketAsync(string userId, BuyTicketViewModel model);
}
