using CinemaApp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Web.Areas.Manager.Controllers
{
    [Area(ManagerRoleName)]
    [Route("Manager/[controller]/[action]")]
    [Authorize(Roles = ManagerRoleName)]
    public class ManagerBaseController : BaseController
    {

    }
}
