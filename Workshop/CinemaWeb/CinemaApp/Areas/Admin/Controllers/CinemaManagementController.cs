using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.Areas.Administrator.Controllers;
using CinemaApp.Web.ViewModels.Admin.CinemaManagement;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Admin.Controllers;

public class CinemaManagementController : AdminBaseController
{
    private readonly ICinemaManagementService _cinemaManagementService;

    public CinemaManagementController(ICinemaManagementService cinemaManagementService)
        => _cinemaManagementService = cinemaManagementService;

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        ICollection<CinemaManagementIndexViewModel> model = await _cinemaManagementService.GetAllCinemasReadonlyAsync();
        return View(model);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(AddCinemaFormModel model)
    {
        if (!ModelState.IsValid) return View(model);

        ServiceResult sr = await _cinemaManagementService.AddCinemaAsync(model);

        return RedirectToAction(nameof(Manage));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid cinemaId)
    {
        ServiceResult<EditCinemaFormModel> sr = await _cinemaManagementService.GetCinemaForEditAsync(cinemaId);
        if (!sr.Found) return NotFound();

        return View(sr.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCinemaFormModel model)
    {
        if (!ModelState.IsValid) return View(model);

        ServiceResult sr = await _cinemaManagementService.EditCinemaAsync(model);
        if (!sr.Found) return NotFound();
        if (!sr.Success) return StatusCode(500);

        return RedirectToAction(nameof(Manage));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleDelete(Guid cinemaId)
    {
        ServiceResult sr = await _cinemaManagementService.ToggleDeleteAsync(cinemaId);
        if (!sr.Found) return NotFound();
        if (!sr.Success) return StatusCode(500);

        return RedirectToAction(nameof(Manage));
    }
}