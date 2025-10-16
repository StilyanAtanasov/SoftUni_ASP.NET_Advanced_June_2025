using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.ProgramSetup;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Manager;

public class ProgramSetupService : IProgramSetupService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ICinemaMovieRepository _cinemaMovieRepository;

    public ProgramSetupService(
        ICinemaMovieRepository cinemaMovieRepository,
        ICinemaRepository cinemaRepository,
        IMovieRepository movieRepository)
    {
        _cinemaMovieRepository = cinemaMovieRepository;
        _cinemaRepository = cinemaRepository;
        _movieRepository = movieRepository;
    }

    public async Task<ServiceResult<ProgramSetupUpdateViewModel>> GetAllMoviesReadonlyAsync(Guid cinemaId)
    {
        Cinema? cinema = await _cinemaRepository.GetByIdAsync(cinemaId);
        if (cinema is null) return ServiceResult<ProgramSetupUpdateViewModel>.NotFound();

        Guid[] cinemaMovies = await _cinemaMovieRepository
            .GetAllReadonly()
            .Where(cm => cm.CinemaId == cinemaId)
            .Select(cm => cm.MovieId)
            .ToArrayAsync();

        ProgramSetupUpdateViewModel model = new()
        {
            CinemaId = cinemaId,
            Movies = _movieRepository
                    .GetAllReadonly()
                    .Select(m => new ProgramSetupMovieViewModel
                    {
                        MovieId = m.Id,
                        Title = m.Title,
                        Duration = m.Duration,
                        PosterUrl = m.ImageUrl,
                        IsIncluded = cinemaMovies.Any(i => i == m.Id)
                    })
                    .ToArray()
        };

        return ServiceResult<ProgramSetupUpdateViewModel>.Ok(model);
    }

    public async Task<ServiceResult> SaveProgramChanges(ProgramSetupUpdateViewModel model)
    {
        Cinema? cinema = await _cinemaRepository.GetByIdAsync(model.CinemaId);
        if (cinema is null) return ServiceResult.NotFound();

        Guid[] cinemaMovies = await _cinemaMovieRepository
            .GetAllReadonly()
            .Where(cm => cm.CinemaId == model.CinemaId)
            .Select(cm => cm.MovieId)
            .ToArrayAsync();

        foreach (ProgramSetupMovieViewModel movie in model.Movies)
        {
            if (movie.IsIncluded)
            {
                if (cinemaMovies.Any(i => i == movie.MovieId)) continue;

                await _cinemaMovieRepository.AddAsync(new CinemaMovie
                {
                    CinemaId = model.CinemaId,
                    MovieId = movie.MovieId,
                });

                continue;
            }

            if (!cinemaMovies.Any(i => i == movie.MovieId)) continue;
            CinemaMovie cm = (await _cinemaMovieRepository
                .FindByConditionsAsync(cm => cm.CinemaId == model.CinemaId && cm.MovieId == movie.MovieId))!;

            await _cinemaMovieRepository.DeleteAsync(cm);
        }

        await _cinemaMovieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}