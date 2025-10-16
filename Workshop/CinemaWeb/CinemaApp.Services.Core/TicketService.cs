using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Ticket;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository) => _ticketRepository = ticketRepository;

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
}