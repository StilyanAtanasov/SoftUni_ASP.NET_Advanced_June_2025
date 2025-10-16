using CinemaApp.Web.ViewModels.User.Cinema;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface ICinemaService
    {
        Task<ICollection<UsersCinemaIndexViewModel>> GetAllCinemasReadonlyAsync();

        Task<CinemaProgramViewModel> GetCinemaProgramReadonlyAsync(Guid cinemaId, string? userId);
    }
}
