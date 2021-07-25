namespace BoostUp.Models.Jobs
{
    using BoostUp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Job;
        
    public class JobInputModel
    {
        [Display(Name = "Job title")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string JobTitle { get; init; }

        [Display(Name = "Employment type")]
        [Required(ErrorMessage = "{0} is required.")]
        public EmploymentType EmploymentType { get; init; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(MinDescriptionLength, ErrorMessage = "{0} must be with a minimum length of {1}.")]
        public string Description { get; init; }

        [Display(Name = "Salary range from:")]
        [Range(MinSalaryRange, MaxSalaryRange, ErrorMessage = SalaryRangeMessage)]
        public int? SalaryRangeFrom { get; init; }

        [Display(Name = "Salary range to:")]
        [Range(MinSalaryRange, MaxSalaryRange, ErrorMessage = SalaryRangeMessage)]
        public int? SalaryRangeTo { get; init; }

        public int CompanyId { get; init; }

        public string RecruiterId { get; init; }
    }
}
