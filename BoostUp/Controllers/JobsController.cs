namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Infrastructure;
    using BoostUp.Models.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

        public IActionResult All()
        {
            var jobs = this.data
                .Jobs
                .Select(j => new JobViewModel
                {
                    Id = j.Id,
                    JobTitle = j.JobTitle,
                    RelativeTime = CalculateRelativeTime(j.CreatedOn),
                    CompanyName = j.Company.Name,
                    CompanyCountry = j.Company.Address.Country,
                    CompanyCity = j.Company.Address.City,
                    CompanyLogoUrl = j.Company.LogoUrl
                })
                .ToList();

            return View(jobs);
        }


        public IActionResult Details() => View();

        private bool UserIsRecruiter()
            => this.data
            .Recruiters
            .Any(r => r.UserId == this.User.GetId());


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
            else if (seconds < 50 * MINUTE)
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
