namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data.Models;

    public  class Industries
    {
        public static IEnumerable<Industry> FiveIndustries()
         => Enumerable.Range(0, 5).Select(i => new Industry());
    }
}
