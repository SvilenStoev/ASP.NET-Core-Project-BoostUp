namespace BoostUp.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs;
    using BoostUp.Infrastructure;

    public class JobsController : Controller
    {
        private readonly IJobService jobs;
        private readonly BoostUpDbContext data;

        public JobsController(IJobService jobs, BoostUpDbContext data)
        {
            this.jobs = jobs;
            this.data = data;
        }

        [Authorize]
        public IActionResult Add(int companyId)
        {
            if (!this.UserIsRecruiter())
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { companyId = companyId });
            }

            return View(new JobInputModel
            {
                EmploymentTypes = this.jobs.AllEmploymentTypes()
            });
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

            if (!this.data.EmploymentTypes.Any(et => et.Id == job.EmploymentTypeId))
            {
                this.ModelState.AddModelError(nameof(job.EmploymentTypeId), "Employment type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                job.EmploymentTypes = this.jobs.AllEmploymentTypes();

                return View(job);
            }

            var jobToAdd = new Job
            {
                JobTitle = job.JobTitle,
                EmploymentTypeId = job.EmploymentTypeId,
                Address = new Address
                {
                    Country = job.Address.Country,
                    City = job.Address.City,
                    AddressText = job.Address.AddressText
                },
                SalaryRangeFrom = job.SalaryRangeFrom,
                SalaryRangeTo = job.SalaryRangeTo,
                Description = job.Description,
                CompanyId = job.CompanyId,
                RecruiterId = recruiterId
            };

            this.data.Jobs.Add(jobToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details));
        }

        public IActionResult All([FromQuery] JobsQueryModel query)
        {
            var jobsQuery = this.jobs.All(
                query.CompanyId,
                query.Country,
                query.EmploymentTypeId,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                JobsQueryModel.jobsPerPage);

            var jobCountries = this.jobs.AllJobCountries();
            var jobEmploymentTypes = this.jobs.AllEmploymentTypes();

            query.Jobs = jobsQuery.Jobs;
            query.TotalJobs = jobsQuery.TotalJobs;
            query.Countries = jobCountries;
            query.EmploymentTypes = jobEmploymentTypes;

            return View(query);
        }

        public IActionResult Details() => View();

        private bool UserIsRecruiter()
            => this.data
            .Recruiters
            .Any(r => r.UserId == this.User.GetId());
    }
}
