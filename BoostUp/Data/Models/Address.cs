namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(AddressTextMaxLength)]
        public string AddressText { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
