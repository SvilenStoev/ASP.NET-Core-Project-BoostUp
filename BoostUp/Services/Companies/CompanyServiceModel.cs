namespace BoostUp.Services.Companies
{
    public class CompanyServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public int? Founded { get; set; }

        public string LogoUrl { get; set; }

        public string AddressCity { get; set; }

        public string AddressCountry { get; set; }

        public string CategoryName { get; set; }

        public string IndustryName { get; set; }
    }
}
