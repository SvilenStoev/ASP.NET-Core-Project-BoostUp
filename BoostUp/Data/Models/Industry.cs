﻿namespace BoostUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Industry
    {
        public int Id { get; init; }

        [Required]
        public string Value { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
