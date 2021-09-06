namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.EmploymentType;

    public class EmploymentType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ValueMaxLength)]
        public string Value { get; set; }

        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
