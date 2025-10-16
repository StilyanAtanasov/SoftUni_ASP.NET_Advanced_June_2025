using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Ticket;
using CinemaApp.Web.ViewModels.User.Ticket;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ICinemaMovieRepository _cinemaMovieRepository;

    public TicketService(ITicketRepository ticketRepository, ICinemaMovieRepository cinemaMovieRepository)
    {
        _ticketRepository = ticketRepository;
        _cinemaMovieRepository = cinemaMovieRepository;
    }

    public async Task<ICollection<UserTicketViewModel>> GetUserTicketsReadonlyAsync(string userId)
        => await _ticketRepository
            .GetAllReadonly()
            .Where(t => t.UserId == userId)
            .Include(t => t.Cinema)
            .Include(t => t.Movie)
            .Select(t => new UserTicketViewModel
            {
                Id = t.Id,
                ImageUrl = t.Movie.ImageUrl,
                MovieTitle = t.Movie.Title,
                CinemaName = t.Cinema.Name,
                Price = t.Price,
                TicketCount = t.Quantity
            })
            .ToArrayAsync();

    public async Task<ServiceResult> BuyTicketAsync(string userId, BuyTicketViewModel model)
    {
        CinemaMovie? cinemaMovie = await _cinemaMovieRepository
            .GetAllReadonly()
            .FirstOrDefaultAsync(cm => cm.MovieId == model.MovieId && cm.CinemaId == model.CinemaId);

        if (cinemaMovie == null) return ServiceResult.BadRequest();

        if (cinemaMovie.AvailableTickets < model.Quantity)
            return ServiceResult.BadRequest(
                new Dictionary<string, string> { { "Quantity", "Not enough available tickets." } });

        Ticket ticket = new()
        {
            UserId = userId,
            CinemaId = model.CinemaId,
            MovieId = model.MovieId,
            Quantity = model.Quantity,
            Price = 0 * model.Quantity,
        };

        cinemaMovie.AvailableTickets -= model.Quantity;

        await _ticketRepository.AddAsync(ticket);
        await _cinemaMovieRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}