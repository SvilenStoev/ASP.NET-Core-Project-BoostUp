namespace BoostUp.Services.Companies
{
    using System.Collections.Generic;

    public class CompanyQueryServiceModel
    {
        public int CurrentPage { get; init; } = 1;

        public int TotalCompanies { get; set; }

        public int IndustryId { get; init; }

        public IEnumerable<CompanyIndustryServiceModel> Industries { get; set; }

        public string SearchTerm { get; init; }

        public string Country { get; init; }

        public IEnumerable<string> Countries { get; set; }

        public IEnumerable<CompanyServiceModel> Companies { get; init; }
    }
}
