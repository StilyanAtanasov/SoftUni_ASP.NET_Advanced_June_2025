using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service) => _service = service;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MovieCardViewModel> movies = await _service.GetAllMoviesAsync(GetUserId());
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _service.AddMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DetailsPartial(Guid id)
        {
            MovieDetailsViewModel? model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            return PartialView("_MovieDetailsPartial", model);
        }
    }
}
