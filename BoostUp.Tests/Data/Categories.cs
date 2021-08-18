namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data.Models;

    public class Categories
    {
        public static IEnumerable<Category> FiveCategories()
            => Enumerable.Range(0, 5).Select(i => new Category());
    }
}
