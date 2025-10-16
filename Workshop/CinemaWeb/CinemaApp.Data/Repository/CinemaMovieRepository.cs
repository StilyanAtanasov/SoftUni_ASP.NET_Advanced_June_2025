using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository;

public class CinemaMovieRepository : BaseRepository<CinemaMovie, Guid>, ICinemaMovieRepository
{
    public CinemaMovieRepository(CinemaAppDbContext dbContext) : base(dbContext) { }
}
