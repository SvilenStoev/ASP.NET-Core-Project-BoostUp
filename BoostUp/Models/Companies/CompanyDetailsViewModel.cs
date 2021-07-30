namespace BoostUp.Models.Companies
{
    public class CompanyDetailsViewModel
    {
        public int CompanyId { get; init; }

        public string Name { get; set; }

        public int? Founded { get; set; }

        public string Overview { get; set; }

        public string LogoUrl { get; set; }

        public string WebsiteUrl { get; set; }

        public string AddressCity { get; set; }

        public string AddressCountry { get; set; }

        public string AddressText { get; set; }

        public string CategoryName { get; set; }

        public string IndustryName { get; set; }

        public int JobsCount { get; set;  }
    }
}
