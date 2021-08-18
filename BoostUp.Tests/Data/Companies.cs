namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data.Models;

    public class Companies
    {
        public static IEnumerable<Company> FiveCompanies()
             => Enumerable.Range(0, 5).Select(i => new Company { IsPublic = true });
    }
}
