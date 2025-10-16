using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Manager.TicketManagement;

namespace CinemaApp.Services.Core.Manager.Contracts;

public interface IManagerTicketService
{
    Task<ICollection<TicketManagementIndexViewModel>> GetAllEligibleCinemasReadonlyAsync();

    Task<ICollection<CinemaMovieTicketsCountViewModel>> GetCinemaMoviesTicketsCountReadonlyAsync(Guid cinemaId);
    
    Task<ServiceResult> UpdateCinemaMoviesTicketsAsync(UpdateCinemaMovieTicketsCountViewModel model);
}
