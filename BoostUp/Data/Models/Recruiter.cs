namespace BoostUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Recruiter
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string FullName { get; init; }

        [Required]
        public string Email { get; init; }

        public string PhoneNumber { get; init; }

        [Required]
        public string UserId { get; init; }

        public IEnumerable<Job> Jobs { get; init; } = new List<Job>();

        //TODO: Recruiter си има една компания, но една компания може да има много Recruiter! Той си избира компанията в Become recruiter.
        public int CompanyId { get; init; }

        public Company Company { get; init; }
    }
}
