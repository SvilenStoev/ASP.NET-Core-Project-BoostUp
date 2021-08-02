namespace BoostUp.Services.Jobs.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobQueryServiceModel
    {
        public int CurrentPage { get; init; } = 1;

        public int TotalJobs { get; set; }

        public string SearchTerm { get; set; }

        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }

        public int EmploymentTypeId { get; set; }

        public IEnumerable<JobEmploymentTypeServiceModel> EmploymentTypes { get; set; }

        public int CompanyId { get; set; }

        public IEnumerable<JobServiceModel> Jobs { get; set; }
    }
}
