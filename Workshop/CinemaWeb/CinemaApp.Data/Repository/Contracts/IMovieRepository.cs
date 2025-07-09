using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts;

public interface IMovieRepository : IRepository<Movie, Guid>
{
    Task<ICollection<Movie>> GetByGenreReadonlyAsync(string genre);
}