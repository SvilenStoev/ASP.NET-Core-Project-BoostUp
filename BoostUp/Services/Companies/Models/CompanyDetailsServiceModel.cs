namespace BoostUp.Services.Companies.Models
{
    public class CompanyDetailsServiceModel : CompanyServiceModel
    {
        public string Overview { get; set; }

        public string WebsiteUrl { get; set; }

        public string AddressText { get; set; }

        public int JobsCount { get; set;  }
    }
}
