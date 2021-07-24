namespace BoostUp.Models.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using static Data.DataConstants.Company;

    public class CompanyInputModel
    {
        [Display(Name = "Company name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string Name { get; init; }

        [Display(Name = "Year of establishment")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(MinYearFounded, MaxYearFounded, ErrorMessage = "{0} must be between {1} and {2}.")]
        public int? Founded { get; init; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(MinOverviewLength, ErrorMessage = "{0} must be with a minimum length of {1}.")]
        public string Overview { get; init; }

        [Url]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; init; }

        [Url]
        [Display(Name = "Website")]
        public string WebsiteUrl { get; init; }

        public AddressInputModel Address { get; init; }

        public int IndustryId { get; init; }

        [BindNever]
        public IEnumerable<CompanyIndustryViewModel> Industries { get; set; }

        public int CategoryId { get; init; }

        [BindNever]
        public IEnumerable<CompanyCategoryViewModel> Categories { get; set; }
    }
}
