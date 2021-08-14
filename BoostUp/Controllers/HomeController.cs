namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BoostUp.Services.Statistics;

    public class HomeController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult Error() 
            => View();
    }
}
