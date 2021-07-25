﻿namespace BoostUp.Models.Recruiters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Recruiter;

    public class RecruiterInputModel
    {
        public string Id { get; init; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public int CompanyId { get; set; }
    }
}
