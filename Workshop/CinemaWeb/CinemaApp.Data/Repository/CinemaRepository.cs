using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository
{
    public class CinemaRepository : BaseRepository<Cinema, Guid>, ICinemaRepository
    {
        public CinemaRepository(CinemaAppDbContext dbContext) : base(dbContext) { }

    }
}