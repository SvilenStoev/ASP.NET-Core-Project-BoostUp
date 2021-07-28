namespace BoostUp.Models.Jobs
{
    using System.Collections.Generic;

    public class JobsQueryModel
    {
        public string SeachTerm { get; set; }

        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }

        public IEnumerable<JobViewModel> Jobs { get; set; }
    }
}
