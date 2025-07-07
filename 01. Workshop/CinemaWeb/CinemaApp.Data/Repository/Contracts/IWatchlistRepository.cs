using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts;

public interface IWatchlistRepository : IRepository<UserMovie, string>
{
    Task<UserMovie?> GetByCompostiteKeyAsync(string userId, string movieId);

    Task<bool> ExistsAsync(string userId, string movieId);
}