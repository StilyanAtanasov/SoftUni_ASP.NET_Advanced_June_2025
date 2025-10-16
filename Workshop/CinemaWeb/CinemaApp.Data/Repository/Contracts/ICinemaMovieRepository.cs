using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts;

public interface ICinemaMovieRepository : IRepository<CinemaMovie, Guid>
{
}
