using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers;

public class ManagerController : BaseController
{
    public IActionResult Index()
    {
        return View("~/Views/Manager/Home/Index.cshtml");
    }
}
