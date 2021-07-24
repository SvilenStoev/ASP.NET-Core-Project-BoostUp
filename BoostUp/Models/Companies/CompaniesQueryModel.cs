namespace BoostUp.Models.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Filter by country")]
        public string Country { get; init; }

        public IEnumerable<string> Countries { get; set; }

        public CompanySorting Sorting { get; init; }

        public IEnumerable<CompanyViewModel> Companies { get; set; }
    }
}
