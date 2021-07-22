namespace BoostUp.Models.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompaniesQueryModel
    {
        public const int companiesPerPage = 8;

        public int CurrentPage { get; init; } = 1;

        public int TotalCompanies { get; set; }

        [Display(Name = "Filter by industry")]
        public string Industry { get; init; }

        public IEnumerable<CompanyIndustryViewModel> Industries { get; set; }

        [Display(Name = "Search by name or year of establishment")]
        public string SearchTerm { get; init; }

        [Display(Name = "Filter by city")]
        public string City { get; init; }

        public IEnumerable<string> Cities { get; set; }

        public CompanySorting Sorting { get; init; }

        public IEnumerable<CompanyViewModel> Companies { get; set; }
    }
}
