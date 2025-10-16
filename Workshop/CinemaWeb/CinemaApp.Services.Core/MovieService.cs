using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static CinemaApp.Data.Common.EntityConstraints.Movie;

namespace CinemaApp.Services.Core;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IWatchlistService _watchlistService;

    public MovieService(IMovieRepository movieRepository, IWatchlistService watchlistService)
    {
        _movieRepository = movieRepository;
        _watchlistService = watchlistService;
    }

    public async Task<IEnumerable<MovieCardViewModel>> GetAllMoviesAsync(string? userId)
    {
        MovieCardViewModel[] movies = await _movieRepository
            .GetAllAttached()
            .AsNoTracking()
            .Where(m => !m.IsDeleted)
            .Select(m => new MovieCardViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Duration = m.Duration.ToString(),
                Genre = m.Genre,
                ImageUrl = m.ImageUrl,
                ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd"),
                IsInWatchlist = false // temporary placeholder
            })
            .ToArrayAsync();

        if (userId != null)
        {
            ICollection<Guid> watchlistMovieIds = await _watchlistService.GetUserWatchlistMovieIdsAsync(userId);
            foreach (MovieCardViewModel movie in movies)
                movie.IsInWatchlist = watchlistMovieIds.Contains(movie.Id);
        }

        return movies;
    }

    public async Task AddMovieAsync(MovieFormViewModel model)
    {
        Movie movie = new()
        {
            Title = model.Title,
            Director = model.Director,
            Duration = model.Duration,
            Description = model.Description,
            Genre = model.Genre,
            ImageUrl = model.ImageUrl,
            ReleaseDate = DateTime.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture)
        };

        await _movieRepository.AddAsync(movie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task<MovieDetailsViewModel?> GetByIdAsync(Guid id)
    {
        Movie? movie = await _movieRepository
            .FirstOrDefaultReadonlyAsync(m => m.Id == id && !m.IsDeleted);

        if (movie == null) return null;

        MovieDetailsViewModel viewModel = new()
        {
            Id = movie.Id.ToString(),
            Title = movie.Title,
            Director = movie.Director,
            Duration = movie.Duration,
            Description = movie.Description,
            Genre = movie.Genre,
            ImageUrl = movie.ImageUrl,
            ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
        };

        return viewModel;
    }
}