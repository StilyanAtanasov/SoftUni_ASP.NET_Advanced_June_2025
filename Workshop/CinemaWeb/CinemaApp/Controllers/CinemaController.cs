using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.User.Cinema;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : BaseController
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService) => _cinemaService = cinemaService;

        public async Task<IActionResult> Index()
        {
            ICollection<UsersCinemaIndexViewModel> model = await _cinemaService.GetAllCinemasReadonlyAsync();
            return View(model);
        }

        public async Task<IActionResult> Program(Guid cinemaId)
        {
            CinemaProgramViewModel model = await _cinemaService.GetCinemaProgramReadonlyAsync(cinemaId);
            if (model.Movies.Count == 0) return RedirectToAction(nameof(Index));
            return View(model);
        }
    }
}
