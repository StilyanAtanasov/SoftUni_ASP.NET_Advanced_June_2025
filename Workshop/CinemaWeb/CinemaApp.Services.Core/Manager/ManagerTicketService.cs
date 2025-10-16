using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.TicketManagement;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Manager;

public class ManagerTicketService : IManagerTicketService
{
    private readonly ICinemaMovieRepository _cinemaMovieRepository;
    private readonly ICinemaRepository _cinemaRepository;

    public ManagerTicketService(ICinemaMovieRepository cinemaMovieRepository, ICinemaRepository cinemaRepository)
    {
        _cinemaMovieRepository = cinemaMovieRepository;
        _cinemaRepository = cinemaRepository;
    }

    public async Task<ICollection<TicketManagementIndexViewModel>> GetAllEligibleCinemasReadonlyAsync()
        => await _cinemaRepository
            .GetAllReadonly()
        .OrderBy(c => c.Location)
            .Select(c => new TicketManagementIndexViewModel()
            {
                Id = c.Id,
                Location = c.Location,
                Name = c.Name,
                HasMovies = c.CinemasMovies.Any(cm => cm.Showtime != "00000")
            })
            .ToArrayAsync();

    public async Task<ICollection<CinemaMovieTicketsCountViewModel>> GetCinemaMoviesTicketsCountReadonlyAsync(Guid cinemaId)
        => await _cinemaMovieRepository
            .GetAllReadonly()
            .Where(cm => cm.CinemaId == cinemaId && cm.Showtime != "00000")
            .Include(cm => cm.Movie)
            .Select(cm => new CinemaMovieTicketsCountViewModel()
            {
                Id = cm.MovieId,
                CinemaId = cm.CinemaId,
                Title = cm.Movie.Title,
                AvailableTickets = cm.AvailableTickets
            })
            .ToArrayAsync();

    public async Task<ServiceResult> UpdateCinemaMoviesTicketsAsync(UpdateCinemaMovieTicketsCountViewModel model)
    {
        CinemaMovie? movie = await _cinemaMovieRepository
            .FirstOrDefaultAsync(m => m.MovieId == model.MovieId && m.CinemaId == model.CinemaId);
        if (movie is null) return ServiceResult.BadRequest();

        movie.AvailableTickets = model.AvailableTickets;
        await _cinemaMovieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}