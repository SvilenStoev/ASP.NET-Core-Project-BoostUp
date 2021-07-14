namespace BoostUp.Data.Models
{
    using System.Collections.Generic;

    public class Industry
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
