namespace BoostUp.Models.Companies
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CompanyAddressViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(AddressCityMaxLength, MinimumLength = AddressCityMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string City { get; init; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(AddressCountryMaxLength, MinimumLength = AddressCountryMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string Country { get; init; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(AddressTextMaxLength, MinimumLength = AddressTextMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string AddressText { get; init; }
    }
}
