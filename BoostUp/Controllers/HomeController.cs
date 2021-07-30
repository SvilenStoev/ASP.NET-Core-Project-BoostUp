namespace BoostUp.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    using BoostUp.Models;
    using BoostUp.Models.Home;
    using BoostUp.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics) 
            => this.statistics = statistics;

        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalCompanies = totalStatistics.TotalCompanies,
                TotalJobs = totalStatistics.TotalJobs,
                TotalUsers = totalStatistics.TotalUsers,
                TotalRecruiters = totalStatistics.TotalRecruiters
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
