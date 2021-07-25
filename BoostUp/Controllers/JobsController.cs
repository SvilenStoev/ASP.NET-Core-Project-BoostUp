namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Infrastructure;
    using BoostUp.Models.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class JobsController : Controller
    {
        private readonly BoostUpDbContext data;

        public JobsController(BoostUpDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Add(int companyId)
        {
            if (!this.UserIsRecruiter())
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { companyId = companyId });
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(JobInputModel job)
        {
            var recruiterId = this.data
                .Recruiters
                .Where(r => r.UserId == this.User.GetId())
                .Select(r => r.Id)
                .FirstOrDefault();

            if (recruiterId == null)
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { value = "companyId" });
            }

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
                Description = job.Description,
                RecruiterId = recruiterId
            };

            this.data.Jobs.Add(jobToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details));
        }

        public IActionResult Details() => View();

        public IActionResult All() => View();

        private bool UserIsRecruiter()
            => this.data
            .Recruiters
            .Any(r => r.UserId == this.User.GetId());
    }
}
