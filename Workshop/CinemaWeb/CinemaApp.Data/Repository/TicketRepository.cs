using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository;

public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
{
    public TicketRepository(CinemaAppDbContext dbContext) : base(dbContext) {}
}
