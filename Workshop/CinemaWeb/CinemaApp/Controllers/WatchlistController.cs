using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
	public class WatchlistController : BaseController
	{
		private readonly IWatchlistService _service;

		public WatchlistController(IWatchlistService service) => _service = service;

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string? userId = GetUserId();
			if (!IsUserAuthenticated() || userId is null) return RedirectToAction(nameof(Index), "Home");

			IEnumerable<WatchlistViewModel> watchlist = await _service.GetUserWatchlistAsync(userId);
			return View(watchlist);
		}

		[HttpPost]
		public async Task<IActionResult> Add(string movieId)
		{
			string? userId = GetUserId();
			if (!IsUserAuthenticated() || userId is null) return RedirectToAction(nameof(Index), "Home");

			if (!await _service.IsMovieInWatchlistAsync(userId, Guid.Parse(movieId)))
				await _service.AddToWatchlistAsync(userId, movieId);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Remove(string movieId)
		{
			string? userId = GetUserId();
			if (!IsUserAuthenticated() || userId is null) return RedirectToAction(nameof(Index), "Home");

			if (await _service.IsMovieInWatchlistAsync(userId, Guid.Parse(movieId)))
				await _service.RemoveFromWatchlistAsync(userId, movieId);

			return RedirectToAction(nameof(Index));
		}
	}
}
