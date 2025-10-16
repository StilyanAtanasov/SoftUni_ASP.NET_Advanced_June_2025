using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.ViewModels.Admin.MovieManagement;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Admin;

public class MovieManagementService : IMovieManagementService
{
    private readonly IMovieRepository _movieRepository;

    public MovieManagementService(IMovieRepository movieRepository)
        => _movieRepository = movieRepository;

    public async Task<ServiceResult> AddMovieAsync(AddMovieInputModel model)
    {
        Movie movie = new()
        {
            Title = model.Title,
            Description = model.Description,
            Duration = model.Duration,
            Genre = model.Genre,
            ImageUrl = model.ImageUrl,
            ReleaseDate = DateTime.Parse(model.ReleaseDate),
            Director = model.Director
        };

        _movieRepository.Add(movie);
        await _movieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> EditMovieAsync(EditMovieFormModel model)
    {
        Movie? movie = await _movieRepository.GetByIdAsync(model.Id);
        if (movie is null) return ServiceResult.NotFound();

        movie.Title = model.Title;
        movie.Description = model.Description;
        movie.Duration = model.Duration;
        movie.Genre = model.Genre;
        movie.ImageUrl = model.ImageUrl;
        movie.ReleaseDate = model.ReleaseDate;
        movie.Director = model.Director;

        await _movieRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ICollection<MovieManagementIndexViewModel>> GetAllMoviesReadonlyAsync()
     => await _movieRepository
        .GetAllReadonly()
        .Select(m => new MovieManagementIndexViewModel
        {
            Id = m.Id,
            Title = m.Title,
            Duration = m.Duration,
            Genre = m.Genre,
            ReleaseDate = m.ReleaseDate.ToString(),
            Director = m.Director,
            IsDeleted = m.IsDeleted,
        })
        .ToArrayAsync();

    public async Task<ServiceResult<EditMovieFormModel>> GetMovieForEditAsync(Guid movieId)
    {
        Movie? movie = await _movieRepository.GetByIdAsync(movieId);
        if (movie is null) return ServiceResult<EditMovieFormModel>.NotFound();

        EditMovieFormModel model = new()
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Duration = movie.Duration,
            Genre = movie.Genre,
            ImageUrl = movie.ImageUrl,
            ReleaseDate = movie.ReleaseDate,
            Director = movie.Director
        };

        return ServiceResult<EditMovieFormModel>.Ok(model);
    }

    public async Task<ServiceResult> ToggleDeleteAsync(Guid movieId)
    {
        Movie? movie = await _movieRepository.GetByIdAsync(movieId);
        if (movie is null) return ServiceResult.NotFound();

        movie.IsDeleted = !movie.IsDeleted;
        await _movieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}
