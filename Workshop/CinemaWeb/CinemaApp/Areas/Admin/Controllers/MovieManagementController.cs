using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.Areas.Administrator.Controllers;
using CinemaApp.Web.ViewModels.Admin.MovieManagement;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Admin.Controllers
{
    public class MovieManagementController : AdminBaseController
    {
        private readonly IMovieManagementService _movieManagementService;

        public MovieManagementController(IMovieManagementService cinemaManagementService)
            => _movieManagementService = cinemaManagementService;

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            ICollection<MovieManagementIndexViewModel> model = await _movieManagementService.GetAllMoviesReadonlyAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ServiceResult sr = await _movieManagementService.AddMovieAsync(model);

            return RedirectToAction(nameof(Manage));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid movieId)
        {
            ServiceResult<EditMovieFormModel> sr = await _movieManagementService.GetMovieForEditAsync(movieId);
            if (!sr.Found) return NotFound();

            return View(sr.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMovieFormModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ServiceResult sr = await _movieManagementService.EditMovieAsync(model);
            if (!sr.Found) return NotFound();
            if (!sr.Success) return StatusCode(500);

            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleDelete(Guid movieId)
        {
            ServiceResult sr = await _movieManagementService.ToggleDeleteAsync(movieId);
            if (!sr.Found) return NotFound();
            if (!sr.Success) return StatusCode(500);

            return RedirectToAction(nameof(Manage));
        }
    }
}
