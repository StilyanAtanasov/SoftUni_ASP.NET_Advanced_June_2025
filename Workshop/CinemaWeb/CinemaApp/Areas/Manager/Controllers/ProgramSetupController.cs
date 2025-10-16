using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.ProgramSetup;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Controllers;

public class ProgramSetupController : ManagerBaseController
{
    private readonly IProgramSetupService _programSetupService;

    public ProgramSetupController(IProgramSetupService programSetupService)
        => _programSetupService = programSetupService;

    [HttpGet]
    public async Task<IActionResult> Index(Guid cinemaId)
    {
        ServiceResult<ProgramSetupUpdateViewModel> sr = await _programSetupService.GetAllMoviesReadonlyAsync(cinemaId);
        if (!sr.Found) return NotFound();
        
        return View(sr.Result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveProgramChanges(ProgramSetupUpdateViewModel model)
    {
        ServiceResult sr = await _programSetupService.SaveProgramChanges(model);
        if (!sr.Found) return NotFound();

        return RedirectToAction(nameof(Index), new { cinemaId = model.CinemaId });
    }
}