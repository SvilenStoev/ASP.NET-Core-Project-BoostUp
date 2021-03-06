using System.Collections.Generic;

namespace BoostUp.Services.Companies.Models
{
    public class CompanyDetailsServiceModel : CompanyServiceModel
    {
        public string Overview { get; set; }

        public string WebsiteUrl { get; set; }

        public string AddressText { get; set; }

        public int JobsCount { get; set; }

        public int CategoryId { get; set; }

        public int IndustryId { get; set; }

        public int EmployeesCount { get; set; }

        public string UserFirstName { get; set; }

        public bool UserIsEmployed { get; set; }
    }
}
