namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Infrastructure;
    using BoostUp.Models.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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

            return View(new JobInputModel
            {
                EmploymentTypes = this.GetEmploymentTypes()
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
                job.EmploymentTypes = this.GetEmploymentTypes();

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

        public IActionResult All(JobsQueryModel query)
        {
            var jobsQuery = this.data.Jobs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Country))
            {
                jobsQuery = jobsQuery.Where(j => j.Address.Country == query.Country);
            }

            if (query.EmploymentTypeId > 0)
            {
                jobsQuery = jobsQuery.Where(j => j.EmploymentType.Id == query.EmploymentTypeId);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                jobsQuery = jobsQuery.Where(j =>
                    j.Address.City.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    j.JobTitle.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            jobsQuery = query.Sorting switch
            {
                JobSorting.DateCreated => jobsQuery.OrderByDescending(j => j.Id),
                JobSorting.Salary => jobsQuery.OrderByDescending(j => j.SalaryRangeFrom),
                JobSorting.JobTitle => jobsQuery.OrderBy(j => j.JobTitle),
                _ => jobsQuery.OrderByDescending(j => j.Id)
            };

            var jobs = jobsQuery
                .Skip((query.CurrentPage - 1) * JobsQueryModel.jobsPerPage)
                .Take(JobsQueryModel.jobsPerPage)
                .Select(j => new JobViewModel
                {
                    Id = j.Id,
                    JobTitle = j.JobTitle,
                    EmploymentType = j.EmploymentType.Value,
                    SalaryRangeFrom = j.SalaryRangeFrom,
                    SalaryRangeTo = j.SalaryRangeTo,
                    CompanyName = j.Company.Name,
                    AddressCountry = j.Address.Country,
                    AddressCity = j.Address.City,
                    CompanyLogoUrl = j.Company.LogoUrl,
                    RelativeTime = CalculateRelativeTime(j.CreatedOn)
                })
                .ToList();

            var jobCountries = this.data
                .Jobs
                .Select(j => j.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

            var jobEmploymentTypes = GetEmploymentTypes();
            var totalJobs = jobsQuery.Count();

            query.Jobs = jobs;
            query.Countries = jobCountries;
            query.EmploymentTypes = jobEmploymentTypes;
            query.TotalJobs = totalJobs;

            return View(query);
        }


        public IActionResult Details() => View();

        private bool UserIsRecruiter()
            => this.data
            .Recruiters
            .Any(r => r.UserId == this.User.GetId());


        private IEnumerable<JobEmploymentTypeViewModel> GetEmploymentTypes()
         => this.data
         .EmploymentTypes
         .Select(et => new JobEmploymentTypeViewModel
         {
             Id = et.Id,
             Value = et.Value
         })
         .ToList();

        private static string CalculateRelativeTime(DateTime createdOn)
        {
            int SECOND = 1;
            int MINUTE = 60 * SECOND;
            int HOUR = 60 * MINUTE;
            int DAY = 24 * HOUR;
            int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - createdOn.Ticks);
            double seconds = Math.Abs(ts.TotalSeconds);

            var relativeTime = "";

            if (seconds < 1 * MINUTE)
            {
                relativeTime = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            else if (seconds < 2 * MINUTE)
            {
                relativeTime = "1 minute ago";

            }
            else if (seconds < 60 * MINUTE)
            {
                relativeTime = ts.Minutes + " minutes ago";
            }
            else if (seconds < 90 * MINUTE)
            {
                relativeTime = "1 hour ago";
            }
            else if (seconds < 24 * HOUR)
            {
                relativeTime = ts.Hours + " hours ago";
            }
            else if (seconds < 48 * HOUR)
            {
                relativeTime = "yesterday";
            }
            else if (seconds < 30 * DAY)
            {
                relativeTime = ts.Days + " days ago";
            }
            else if (seconds < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));

                relativeTime = months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                relativeTime = years <= 1 ? "one year ago" : years + " years ago";
            }

            return relativeTime;
        }
    }
}
