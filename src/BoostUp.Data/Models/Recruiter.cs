namespace BoostUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.Recruiter;

    public class Recruiter
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Email { get; set; }

        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Job> Jobs { get; init; } = new List<Job>();

        public int? CompanyId { get; set; }

        public Company Company { get; init; }
    }
}
