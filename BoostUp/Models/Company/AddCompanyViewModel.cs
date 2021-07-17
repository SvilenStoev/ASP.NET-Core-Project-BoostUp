using BoostUp.Data;

namespace BoostUp.Models.Company
{
    using BoostUp.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class AddCompanyViewModel
    {
        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string Name { get; set; }

        public int Founded { get; set; }

        [Required]
        public string Overview { get; set; }

        public string LogoUrl { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
