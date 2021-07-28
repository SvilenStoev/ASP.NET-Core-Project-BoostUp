namespace BoostUp.Models.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class JobsQueryModel
    {
        public const int jobsPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalJobs { get; set; }

        [Display(Name = "Search by job title or city")]
        public string SearchTerm { get; set; }

        [Display(Name = "Filter by country")]
        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }

        [Display(Name = "Filter by employment type")]
        public int EmploymentTypeId { get; set; }

        public IEnumerable<JobEmploymentTypeViewModel> EmploymentTypes { get; set; }

        public JobSorting Sorting { get; set; }

        public IEnumerable<JobViewModel> Jobs { get; set; }
    }
}
