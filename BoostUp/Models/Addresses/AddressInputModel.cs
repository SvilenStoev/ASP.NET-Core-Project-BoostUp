namespace BoostUp.Models.Addresses
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Address;

    public class AddressInputModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"[a-z A-Z]+", ErrorMessage = "{0} should not contains any digits.")]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string City { get; init; }

        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"[a-z A-Z]+", ErrorMessage = "{0} should not contains any digits.")]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string Country { get; init; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(TextMaxLength, MinimumLength = TextMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string AddressText { get; init; }
    }
}
