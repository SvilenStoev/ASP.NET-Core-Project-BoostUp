namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static BoostUp.Common.DataConstants.Industry;

    public class Industry
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ValueMaxLength)]
        public string Value { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
