namespace BoostUp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.Job;

    public class Job
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string JobTitle { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        [Required]
        public string Description { get; set; }

        public int? SalaryRangeFrom { get; set; }

        public int? SalaryRangeTo { get; set; }

        public int Views { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; init; }

        public int EmploymentTypeId { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; init; }

        public string RecruiterId { get; init; }

        public Recruiter Recruiter { get; init; }
    }
}
