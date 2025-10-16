using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Administrator.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
