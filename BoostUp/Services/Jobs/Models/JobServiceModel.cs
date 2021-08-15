namespace BoostUp.Services.Jobs.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobServiceModel : IJobModel
    {
        public int Id { get; init; }

        public string JobTitle { get; set; }

        public string RelativeTime { get; set; }

        public string EmploymentType { get; set; }

        public int? SalaryRangeFrom { get; set; }

        public int? SalaryRangeTo { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public JobCompanyDetailsServiceModel Company { get; set; }
    }
}
