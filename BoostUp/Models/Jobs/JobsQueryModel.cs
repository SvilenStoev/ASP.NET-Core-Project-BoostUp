namespace BoostUp.Models.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using BoostUp.Services.Jobs.Models;

    public class JobsQueryModel
    {
        public const int jobsPerPage = 8;

        public int CurrentPage { get; init; } = 1;

        public int TotalJobs { get; set; }

        [Display(Name = "Search by job title or city")]
        public string SearchTerm { get; set; }

        [Display(Name = "Filter by country")]
        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }

        [Display(Name = "Filter by employment type")]
        public int EmploymentTypeId { get; set; }

        public IEnumerable<JobEmploymentTypeServiceModel> EmploymentTypes { get; set; }

        public JobSorting Sorting { get; set; }

        public int CompanyId { get; set; }

        public IEnumerable<JobServiceModel> Jobs { get; set; }
    }
}
