using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Data.Models.Conversions.MovieShowtimes;

namespace CinemaApp.Services.Core.Manager;

public class ShowtimeSetupService : IShowtimeSetupService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly ICinemaMovieRepository _cinemaMovieRepository;

    public ShowtimeSetupService(ICinemaRepository cinemaRepository, ICinemaMovieRepository cinemaMovieRepository)
    {
        _cinemaRepository = cinemaRepository;
        _cinemaMovieRepository = cinemaMovieRepository;
    }

    public async Task<ICollection<ShowtimeSetupIndexViewModel>> GetAllActiveCinemasReadonlyAsync()
     => await _cinemaRepository
            .GetAllReadonly()
            .Where(c => c.CinemasMovies.Any())
            .Select(c => new ShowtimeSetupIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
            })
            .ToArrayAsync();

    public async Task<ICollection<MovieShowtimesViewModel>> GetMoviesWithShowtimesReadonlyAsync(Guid cinemaId)
    {
        CinemaMovie[] records = await _cinemaMovieRepository
            .GetAllReadonly()
            .Include(c => c.Movie)
            .Where(c => c.CinemaId == cinemaId)
            .ToArrayAsync();

        MovieShowtimesViewModel[] result = [.. records
            .Select(c => new MovieShowtimesViewModel
            {
                Id = c.MovieId,
                Title = c.Movie.Title,
                Showtimes = Hours
                    .Zip(c.Showtime, (hour, flag) => new { hour, flag })
                    .Where(x => x.flag == '1')
                    .Select(x => x.hour)
                    .ToArray()
            })];

        return result;
    }

    public async Task<ServiceResult> UpdateShowtimesAsync(UpdateShowtimesViewModel model)
    {
        CinemaMovie? cm = await _cinemaMovieRepository
            .FirstOrDefaultAsync(cm => cm.MovieId == model.MovieId && cm.CinemaId == model.CinemaId);

        if (cm is null) return ServiceResult.BadRequest();

        cm.Showtime = string.Concat(Hours.Select(h => model.Showtimes.Contains(h) ? '1' : '0'));
        await _cinemaMovieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}