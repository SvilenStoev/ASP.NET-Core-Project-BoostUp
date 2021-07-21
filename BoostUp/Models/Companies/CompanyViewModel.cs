namespace BoostUp.Models.Companies
{
    using BoostUp.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompanyViewModel
    {
        public int Id { get; init; }

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
    }
}
