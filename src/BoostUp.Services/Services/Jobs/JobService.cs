namespace BoostUp.Services.Jobs
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.Extensions.Caching.Memory;

    using Common;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Services.Jobs.Models;

    using static BoostUp.Common.GlobalConstants.Cache;

    public class JobService : IJobService
    {
        private readonly BoostUpDbContext data;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public JobService(BoostUpDbContext data, IMapper mapper, IMemoryCache cache)
        {
            this.data = data;
            this.mapper = mapper;
            this.cache = cache;
        }

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

            var jobs = GetJobs(jobsQuery
                .Skip((currentPage - 1) * jobsPerPage)
                .Take(jobsPerPage));

            var totalJobs = jobsQuery.Count();

            return new JobQueryServiceModel
            {
                Jobs = jobs,
                CurrentPage = currentPage,
                TotalJobs = totalJobs,
            };
        }

        public int Create(
            string jobTitle,
            string country,
            string city,
            string addressText,
            string description,
            string recruiterId,
            int employmentTypeId,
            int? salaryRangeFrom,
            int? salaryRangeTo,
            int companyId)
        {
            var jobToAdd = new Job
            {
                JobTitle = jobTitle,
                Address = new Address
                {
                    Country = country,
                    City = city,
                    AddressText = addressText
                },
                Description = description,
                RecruiterId = recruiterId,
                EmploymentTypeId = employmentTypeId,
                SalaryRangeFrom = salaryRangeFrom,
                SalaryRangeTo = salaryRangeTo,
                CompanyId = companyId,
            };

            this.data.Jobs.Add(jobToAdd);

            this.data.SaveChanges();

            return jobToAdd.Id;
        }

        public bool Edit(
                int id,
                string jobTitle,
                string country,
                string city,
                string addressText,
                string description,
                int employmentTypeId,
                int? salaryRangeFrom,
                int? salaryRangeTo,
                int companyId)
        {
            var job = this.data.Jobs.Find(id);

            var jobAddress = this.data.Addresses.Find(job.AddressId);

            if (job == null)
            {
                return false;
            }

            job.JobTitle = jobTitle;
            jobAddress.Country = country;
            jobAddress.City = city;
            jobAddress.AddressText = addressText;
            job.Description = description;
            job.EmploymentTypeId = employmentTypeId;
            job.SalaryRangeFrom = salaryRangeFrom;
            job.SalaryRangeTo = salaryRangeTo;

            this.data.SaveChanges();

            return true;
        }

        public JobDetailsServiceModel Details(int id)
            => this.data
                .Jobs
                .Where(j => j.Id == id)
                .Select(j => new JobDetailsServiceModel
                {
                    Id = j.Id,
                    JobTitle = j.JobTitle,
                    Country = j.Address.Country,
                    City = j.Address.City,
                    AddressText = j.Address.AddressText,
                    Description = j.Description,
                    SalaryRangeFrom = j.SalaryRangeFrom,
                    SalaryRangeTo = j.SalaryRangeTo,
                    EmploymentTypeId = j.EmploymentTypeId,
                    EmploymentType = j.EmploymentType.Value,
                    RecruiterId = j.RecruiterId,
                    RecruiterEmail = j.Recruiter.Email,
                    RecruiterPhoneNumber = j.Recruiter.PhoneNumber,
                    RecruiterCompanyName = j.Recruiter.Company.Name,
                    UserId = j.Recruiter.UserId,
                    Company = new JobCompanyDetailsServiceModel
                    {
                        Id = j.CompanyId,
                        Category = j.Company.Category.Value,
                        Industry = j.Company.Industry.Value,
                        Name = j.Company.Name,
                        Founded = j.Company.Founded,
                        LogoUrl = j.Company.LogoUrl,
                    },
                    RelativeTime = CalculateRelativeTime(j.CreatedOn),
                })
                .ToList()
                .FirstOrDefault();

        public IEnumerable<JobServiceModel> ByUser(string userId)
            => GetJobs(this.data
                .Jobs
                .Where(j => j.Recruiter.UserId == userId));

        public IEnumerable<string> AllCountries()
            => this.data
                .Jobs
                .Select(j => j.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

        public IEnumerable<JobEmploymentTypeServiceModel> AllEmploymentTypes()
        {
            var employmentTypes = this.cache.Get<IEnumerable<JobEmploymentTypeServiceModel>>(allEmploymentTypesCacheKey);

            if (employmentTypes == null)
            {
                employmentTypes = this.data
                    .EmploymentTypes
                    .ProjectTo<JobEmploymentTypeServiceModel>(this.mapper.ConfigurationProvider)
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                this.cache.Set(allEmploymentTypesCacheKey, employmentTypes, cacheOptions);
            }

            return employmentTypes;
        }

        public int JobViews(int id)
        {
            var job = this.data.Jobs.Find(id);

            if (job == null)
            {
                return 0;
            }

            var jobViews = ++job.Views;

            this.data.SaveChanges();

            return jobViews;
        }

        public string InformationById(int id)
        {
            var sb = new StringBuilder();

            var job = this.data.Jobs.Find(id);

            sb.Append($"{job.JobTitle}");

            return sb.ToString();
        }

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

        private static IEnumerable<JobServiceModel> GetJobs(IQueryable<Job> jobQuery)
            => jobQuery
                .Select(j => new JobServiceModel
                {
                    Id = j.Id,
                    JobTitle = j.JobTitle,
                    EmploymentType = j.EmploymentType.Value,
                    SalaryRangeFrom = j.SalaryRangeFrom,
                    SalaryRangeTo = j.SalaryRangeTo,
                    Company = new JobCompanyDetailsServiceModel
                    {
                        Name = j.Company.Name,
                        LogoUrl = j.Company.LogoUrl,
                    },
                    Country = j.Address.Country,
                    City = j.Address.City,
                    RelativeTime = CalculateRelativeTime(j.CreatedOn)
                })
                .ToList();

        public bool IsByRecruiter(int jobId, string recruiterId)
            => this.data
            .Jobs
            .Any(j => j.Id == jobId && j.RecruiterId == recruiterId);

        public bool EmploymentTypeExists(int employmentTypeId)
            => this.data
            .EmploymentTypes
            .Any(et => et.Id == employmentTypeId);
    }
}
