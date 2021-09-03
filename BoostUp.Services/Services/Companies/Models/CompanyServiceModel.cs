namespace BoostUp.Services.Companies.Models
{
    public class CompanyServiceModel : ICompanyModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public int? Founded { get; set; }

        public string LogoUrl { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CategoryName { get; set; }

        public string IndustryName { get; set; }
    }
}
