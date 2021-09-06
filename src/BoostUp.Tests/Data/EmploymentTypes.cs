namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data.Models;

    public class EmploymentTypes
    {
        public static IEnumerable<EmploymentType> FiveEmploymentTypes()
         => Enumerable.Range(0, 5).Select(i => new EmploymentType());
    }
}
