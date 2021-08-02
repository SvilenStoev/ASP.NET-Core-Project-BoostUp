namespace BoostUp.Services.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BoostUp.Data;
    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs.Models;

    public class JobService : IJobService
    {
        private readonly BoostUpDbContext data;

        public JobService(BoostUpDbContext data)
            => this.data = data;

        public JobQueryServiceModel All(
            int companyId,
            string country,
            int employmentTypeId,
            string searchTerm,
            JobSorting sorting,
            int currentPage,
            int jobsPerPage)
        {
            var jobsQuery = this.data.Jobs.AsQueryable();

            if (companyId > 0)
            {
                jobsQuery = jobsQuery.Where(j => j.CompanyId == companyId);
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                jobsQuery = jobsQuery.Where(j => j.Address.Country == country);
            }

            if (employmentTypeId > 0)
            {
                jobsQuery = jobsQuery.Where(j => j.EmploymentType.Id == employmentTypeId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                jobsQuery = jobsQuery.Where(j =>
                    j.Address.City.ToLower().Contains(searchTerm.ToLower()) ||
                    j.JobTitle.ToLower().Contains(searchTerm.ToLower()));
            }

            jobsQuery = sorting switch
            {
                JobSorting.DateCreated => jobsQuery.OrderByDescending(j => j.Id),
                JobSorting.Salary => jobsQuery.OrderByDescending(j => j.SalaryRangeFrom),
                JobSorting.JobTitle => jobsQuery.OrderBy(j => j.JobTitle),
                _ => jobsQuery.OrderByDescending(j => j.Id)
            };

            var jobs = jobsQuery
                .Skip((currentPage - 1) * jobsPerPage)
                .Take(jobsPerPage)
                .Select(j => new JobServiceModel
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

            var totalJobs = jobsQuery.Count();

            return new JobQueryServiceModel
            {
                Jobs = jobs,
                CurrentPage = currentPage,
                TotalJobs = totalJobs,
            };
        }

        public IEnumerable<string> AllJobCountries()
            => this.data
                .Jobs
                .Select(j => j.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

        public IEnumerable<JobEmploymentTypeServiceModel> AllEmploymentTypes()
            => this.data
                 .EmploymentTypes
                 .Select(et => new JobEmploymentTypeServiceModel
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
