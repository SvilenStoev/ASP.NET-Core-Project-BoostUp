namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Jobs;
    using Microsoft.AspNetCore.Mvc;

    public class JobsController : Controller
    {
        private readonly BoostUpDbContext data;

        public JobsController(BoostUpDbContext data) => this.data = data;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(JobInputModel job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }

            var jobToAdd = new Job
            {
                JobTitle = job.JobTitle,
                SalaryRangeFrom = job.SalaryRangeFrom,
                SalaryRangeTo = job.SalaryRangeTo,
                EmploymentType = job.EmploymentType,
                CompanyId = job.CompanyId,
                RecruiterId = job.RecruiterId,
                Description = job.Description
            };

            this.data.Jobs.Add(jobToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details));
        }

        public IActionResult Details() => View();
    }
}
