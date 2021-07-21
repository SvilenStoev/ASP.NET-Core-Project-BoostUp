namespace BoostUp.Models.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompaniesQueryModel
    {
        [Display(Name = "Filter by industry")]
        public string Industry { get; init; }

        public IEnumerable<CompanyIndustryViewModel> Industries { get; init; }

        [Display(Name = "Filter by city")]
        public string City { get; init; }

        public IEnumerable<string> Cities { get; init; }

        [Display(Name = "Search by name or year")]            
        public string SearchTerm { get; init; }

        public IEnumerable<CompanyViewModel> Companies { get; init; }
    }
}
