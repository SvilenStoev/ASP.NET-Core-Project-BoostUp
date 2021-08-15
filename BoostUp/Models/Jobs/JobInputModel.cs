namespace BoostUp.Models.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BoostUp.Models.Addresses;
    using BoostUp.Services.Jobs.Models;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using static Data.DataConstants.Job;

    public class JobInputModel : IJobModel
    {
        [Display(Name = "Job title")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string JobTitle { get; init; }

        public AddressInputModel Address { get; init; }

        [Display(Name = "Salary range from:")]
        [Range(SalaryMinRange, SalaryMaxRange, ErrorMessage = SalaryRangeMessage)]
        public int? SalaryRangeFrom { get; init; }

        [Display(Name = "Salary range to:")]
        [Range(SalaryMinRange, SalaryMaxRange, ErrorMessage = SalaryRangeMessage)]
        public int? SalaryRangeTo { get; init; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(DescriptionMinLength, ErrorMessage = "{0} must be with a minimum length of {1}.")]
        public string Description { get; init; }

        public int CompanyId { get; init; }

        public int EmploymentTypeId { get; init; }

        [BindNever]
        public string EmploymentType { get; init; }

        [BindNever]
        public IEnumerable<JobEmploymentTypeServiceModel> EmploymentTypes { get; set; }
    }
}
