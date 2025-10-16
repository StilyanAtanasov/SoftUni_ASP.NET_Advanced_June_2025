using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Cinema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IWatchlistService _watchlistService;

        public CinemaService(ICinemaRepository cinemaRepository, IWatchlistService watchlistService)
        {
            _cinemaRepository = cinemaRepository;
            _watchlistService = watchlistService;
        }

        public async Task<ICollection<UsersCinemaIndexViewModel>> GetAllCinemasReadonlyAsync() =>
            await _cinemaRepository
            .GetAllReadonly()
            .Select(c => new UsersCinemaIndexViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            })
            .ToArrayAsync();

        public async Task<CinemaProgramViewModel> GetCinemaProgramReadonlyAsync(Guid cinemaId, string? userId)
        {
            Cinema cinema = await _cinemaRepository
                .GetAllReadonly()
                .Where(c => c.Id == cinemaId)
                .Include(c => c.CinemasMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstAsync();

            CinemaProgramViewModel viewModel = new()
            {
                CinemaId = cinema.Id,
                CinemaName = cinema.Name,
                Movies = new List<CinemaProgramMovieViewModel>()
            };

            foreach (CinemaMovie cm in cinema.CinemasMovies)
            {
                CinemaProgramMovieViewModel movieVm = new()
                {
                    Id = cm.MovieId,
                    Director = cm.Movie.Director,
                    ImageUrl = cm.Movie.ImageUrl,
                    Title = cm.Movie.Title,
                    IsInWatchlist = userId != null &&
                                    await _watchlistService.IsMovieInWatchlistAsync(userId, cm.MovieId)
                };

                viewModel.Movies.Add(movieVm);
            }

            return viewModel;
        }
    }
}
