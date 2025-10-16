using CinemaApp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Web.Areas.Administrator.Controllers
{
    [Area(AdminRoleName)]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = AdminRoleName)]
    public class AdminBaseController : BaseController
    {

    }
}
