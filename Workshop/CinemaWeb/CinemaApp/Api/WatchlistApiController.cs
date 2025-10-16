using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class WatchlistApiController : BaseController
{
    private readonly IWatchlistService _watchlistService;

    public WatchlistApiController(IWatchlistService watchlistService) => _watchlistService = watchlistService;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Add(Guid movieId)
    {
        string userId = GetUserId()!;

        if (!await _watchlistService.IsMovieInWatchlistAsync(userId, movieId))
            await _watchlistService.AddToWatchlistAsync(userId, movieId);
        else return BadRequest();

        return Ok();
    }
}
