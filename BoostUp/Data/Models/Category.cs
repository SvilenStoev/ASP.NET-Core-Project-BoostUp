namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ValueMaxLength)]
        public string Value { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
