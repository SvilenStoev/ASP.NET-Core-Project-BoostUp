namespace BoostUp.Services.Jobs.Models
{
    using BoostUp.Services.Companies.Models;

    public class JobCompanyDetailsServiceModel : ICompanyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Founded { get; set; }

        public string LogoUrl { get; set; }

        public string Category { get; set; }

        public string Industry { get; set; }
    }
}
