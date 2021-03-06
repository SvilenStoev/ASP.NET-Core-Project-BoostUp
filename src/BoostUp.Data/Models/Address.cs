namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.Address;

    public class Address
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [MaxLength(TextMaxLength)]
        public string AddressText { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();

        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();

        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
