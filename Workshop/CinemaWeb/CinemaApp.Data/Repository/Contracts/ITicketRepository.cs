using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts;

public interface ITicketRepository : IRepository<Ticket, Guid>
{
}