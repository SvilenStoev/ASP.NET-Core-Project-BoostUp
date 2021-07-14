namespace BoostUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Company
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string Name { get; set; }

        public int Founded { get; set; }
        
        [Required]
        public string Overview { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public string WebsiteUrl { get; set; }

        public int IndustryId { get; set; }

        public Industry Industry { get; init; }

    } 
}
