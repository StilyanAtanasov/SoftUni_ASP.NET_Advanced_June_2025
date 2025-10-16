using CinemaApp.Web.ViewModels.User.Movie;

namespace CinemaApp.Services.Core.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieCardViewModel>> GetAllMoviesAsync(string? userId);

    Task AddMovieAsync(MovieFormViewModel model);

    Task<MovieDetailsViewModel?> GetByIdAsync(Guid id);
}