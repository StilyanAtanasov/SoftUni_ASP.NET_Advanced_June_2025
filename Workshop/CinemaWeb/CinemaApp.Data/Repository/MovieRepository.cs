using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository;

public class MovieRepository : BaseRepository<Movie, Guid>, IMovieRepository
{
    public MovieRepository(CinemaAppDbContext dbContext) : base(dbContext) { }

    public async Task<ICollection<Movie>> GetByGenreReadonlyAsync(string genre) =>
        await GetCollectionByConditionsReadonlyAsync(m => m.Genre == genre);
}