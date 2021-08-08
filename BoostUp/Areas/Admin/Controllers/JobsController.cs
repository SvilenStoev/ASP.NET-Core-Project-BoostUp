namespace BoostUp.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AreaName)]
    public class JobsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
