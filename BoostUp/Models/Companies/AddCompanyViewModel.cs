namespace BoostUp.Models.Companies
{
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
    }
}
