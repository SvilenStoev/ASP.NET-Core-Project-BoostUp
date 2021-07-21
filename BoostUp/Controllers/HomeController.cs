namespace BoostUp.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using BoostUp.Data;
    using BoostUp.Models;
    using BoostUp.Models.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly BoostUpDbContext data;

        public HomeController(BoostUpDbContext data) => this.data = data;

        public IActionResult Index()
        {
            var totalCompanies = this.data.Companies.Count();
            var totalJobs = this.data.Jobs.Count();
            var totalUsers = this.data.Users.Count();
            var totalRecruiters = this.data.Users.Count();

            return View(new IndexViewModel
            {
                TotalCompanies = totalCompanies,
                TotalJobs = totalJobs,
                TotalUsers = totalUsers,
                TotalRecruiters = totalRecruiters
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
