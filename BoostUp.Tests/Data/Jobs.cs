namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data.Models;

    public static class Jobs
    {
        public static IEnumerable<Job> FiveJobs()
            => Enumerable.Range(0, 5).Select(i => new Job());
    }
}
