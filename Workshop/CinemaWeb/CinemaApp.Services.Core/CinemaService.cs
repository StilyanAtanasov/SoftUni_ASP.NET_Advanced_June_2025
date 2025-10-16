using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Cinema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ICinemaMovieRepository _cinemaMovieRepository;

        public CinemaService(ICinemaRepository cinemaRepository, ICinemaMovieRepository cinemaMovieRepository)
        {
            _cinemaRepository = cinemaRepository;
            _cinemaMovieRepository = cinemaMovieRepository;
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

        public async Task<CinemaProgramViewModel> GetCinemaProgramReadonlyAsync(Guid cinemaId)
            => await _cinemaRepository
            .GetAllReadonly()
            .Where(c => c.Id == cinemaId)
            .Include(c => c.CinemasMovies)
                .ThenInclude(cm => cm.Movie)
            .Select(c => new CinemaProgramViewModel()
            {
                CinemaId = c.Id,
                CinemaName = c.Name,
                Movies = c.CinemasMovies
                .Select(cm => new CinemaProgramMovieViewModel()
                {
                    Id = cm.MovieId,
                    Director = cm.Movie.Director,
                    ImageUrl = cm.Movie.ImageUrl,
                    Title = cm.Movie.Title
                })
                .ToArray()
            })
            .FirstAsync();
    }
}
