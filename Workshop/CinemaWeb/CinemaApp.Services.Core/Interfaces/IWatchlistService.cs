using CinemaApp.Web.ViewModels.User.Watchlist;

namespace CinemaApp.Services.Core.Interfaces;

public interface IWatchlistService
{
    Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

    Task<ICollection<Guid>> GetUserWatchlistMovieIdsAsync(string userId);

    Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId);

    Task AddToWatchlistAsync(string userId, Guid movieId);

    Task RemoveFromWatchlistAsync(string userId, Guid movieId);
}