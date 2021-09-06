namespace BoostUp.Models.Recruiters
{
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.Recruiter;

    public class RecruiterInputModel
    {
        public string Id { get; init; }

        [EmailAddress]
        [Display(Name = "Email for receiving CVs")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public int CompanyId { get; set; }
    }
}
