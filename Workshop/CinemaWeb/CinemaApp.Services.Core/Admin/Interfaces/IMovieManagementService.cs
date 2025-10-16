using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Admin.MovieManagement;

namespace CinemaApp.Services.Core.Admin.Interfaces;

public interface IMovieManagementService
{
    Task<ICollection<MovieManagementIndexViewModel>> GetAllMoviesReadonlyAsync();

    Task<ServiceResult> AddMovieAsync(AddMovieInputModel model);

    Task<ServiceResult<EditMovieFormModel>> GetMovieForEditAsync(Guid movieId);

    Task<ServiceResult> EditMovieAsync(EditMovieFormModel model);

    Task<ServiceResult> ToggleDeleteAsync(Guid movieId);
}
