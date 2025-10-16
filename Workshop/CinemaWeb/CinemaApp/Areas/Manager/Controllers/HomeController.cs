using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Controllers;

public class HomeController : ManagerBaseController
{
    public IActionResult Index()
    {
        return View();
    }
}