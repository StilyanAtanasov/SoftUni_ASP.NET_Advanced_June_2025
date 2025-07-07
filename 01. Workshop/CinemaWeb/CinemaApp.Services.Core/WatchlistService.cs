using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Data.Common.EntityConstraints.Movie;

namespace CinemaApp.Services.Core;

public class WatchlistService : IWatchlistService
{
    private readonly IWatchlistRepository _watchlistRepository;

    public WatchlistService(IWatchlistRepository watchlistRepository) =>
        _watchlistRepository = watchlistRepository;

    public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId) =>
        await _watchlistRepository
            .GetAllAttached()
            .AsNoTracking()
            .Where(um => um.UserId == userId)
            .Select(um => new WatchlistViewModel
            {
                MovieId = um.MovieId.ToString(),
                Title = um.Movie.Title,
                Genre = um.Movie.Genre,
                ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                ImageUrl = um.Movie.ImageUrl,
            })
            .ToArrayAsync();

    public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId) =>
        await _watchlistRepository
            .GetAllAttached()
            .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);

    public async Task AddToWatchlistAsync(string userId, string movieId)
    {
        UserMovie userMovie = new()
        {
            UserId = userId,
            MovieId = Guid.Parse(movieId)
        };

        await _watchlistRepository.AddAsync(userMovie);
        await _watchlistRepository.SaveChangesAsync();
    }

    public async Task RemoveFromWatchlistAsync(string userId, string movieId)
    {
        UserMovie? userMovie = await _watchlistRepository
            .FirstOrDefaultAsync(um => um.MovieId == Guid.Parse(movieId) && um.UserId == userId);

        if (userMovie != null) await _watchlistRepository.DeleteAsync(userMovie);
    }
}