using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Cinema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : BaseController
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService) => _cinemaService = cinemaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ICollection<UsersCinemaIndexViewModel> model = await _cinemaService.GetAllCinemasReadonlyAsync();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Program(Guid cinemaId)
        {
            CinemaProgramViewModel model = await _cinemaService.GetCinemaProgramReadonlyAsync(cinemaId, GetUserId());
            if (model.Movies.Count == 0) return RedirectToAction(nameof(Index));
            return View(model);
        }
    }
}
