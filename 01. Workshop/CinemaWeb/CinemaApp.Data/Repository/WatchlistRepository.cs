using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository;

public class WatchlistRepository : BaseRepository<UserMovie, string>, IWatchlistRepository
{
    public WatchlistRepository(CinemaAppDbContext context) : base (context) { }

    public async Task<bool> ExistsAsync(string userId, string movieId) => 
        await GetAllAttached().AnyAsync(um => um.UserId == userId && um.MovieId.ToString() == movieId);
    
    public async Task<UserMovie?> GetByCompostiteKeyAsync(string userId, string movieId) =>
        await FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId.ToString() == movieId);
}