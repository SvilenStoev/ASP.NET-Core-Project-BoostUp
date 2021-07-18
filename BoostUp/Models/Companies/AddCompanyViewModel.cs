namespace BoostUp.Models.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCompanyViewModel
    {
        [Required]
        public string Name { get; init; }

        public int Founded { get; init; }

        [Required]
        public string Overview { get; init; }

        public string LogoUrl { get; init; }

        public string WebsiteUrl { get; init; }

        public CompanyAddressViewModel Address { get; init; }

        public int IndustryId { get; init; }

        public IEnumerable<CompanyIndustryViewModel> Industries { get; set; }

        public int CategoryId { get; init; }

        public IEnumerable<CompanyCategoryViewModel> Categories { get; set; }
    }
}
