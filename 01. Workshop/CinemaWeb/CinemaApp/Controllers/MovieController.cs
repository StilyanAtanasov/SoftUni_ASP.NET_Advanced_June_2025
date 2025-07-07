using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
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
            IEnumerable<MovieCardViewModel> movies = await _service.GetAllMoviesAsync();
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
        public async Task<IActionResult> Details(string id)
        {
            MovieDetailsViewModel? model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            MovieFormViewModel? model = await _service.GetForEditByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, MovieFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _service.EditAsync(id, model);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            MovieDetailsViewModel? model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
