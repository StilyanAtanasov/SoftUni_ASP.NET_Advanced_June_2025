using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;

namespace CinemaApp.Services.Core.Manager.Contracts;

public interface IShowtimeSetupService
{
    Task<ICollection<ShowtimeSetupIndexViewModel>> GetAllActiveCinemasReadonlyAsync();

    Task<ICollection<MovieShowtimesViewModel>> GetMoviesWithShowtimesReadonlyAsync(Guid cinemaId);

    Task<ServiceResult> UpdateShowtimesAsync(UpdateShowtimesViewModel model);
}