namespace BoostUp.Services.Jobs.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobServiceModel
    {
        public int Id { get; init; }

        public string JobTitle { get; set; }

        public string RelativeTime { get; set; }

        public string EmploymentType { get; set; }

        public int? SalaryRangeFrom { get; set; }

        public int? SalaryRangeTo { get; set; }

        public string AddressCountry { get; set; }

        public string AddressCity { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogoUrl { get; set; }
    }
}
