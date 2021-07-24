namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Company;

    public class Company
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int? Founded { get; set; }
        
        [Required]
        public string Overview { get; set; }

        public string LogoUrl { get; set; }

        public string WebsiteUrl { get; set; }

        public int IndustryId { get; set; }

        public Industry Industry { get; init; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int AddressId { get; set; }

        public Address Address { get; init; }

        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
