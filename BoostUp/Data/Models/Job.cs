namespace BoostUp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Job
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(JobTitleMaxLength)]
        public string JobTitle { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        [Required]
        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; init; }
    }
}
