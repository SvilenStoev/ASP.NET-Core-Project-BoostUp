namespace BoostUp.Models.Jobs
{
    using BoostUp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobViewModel
    {
        public int Id { get; init; }

        public string JobTitle { get; set; }

        public string RelativeTime { get; set; }

        public string CompanyName { get; set; }

        public string CompanyCountry { get; set; }

        public string CompanyCity { get; set; }

        public string CompanyLogoUrl { get; set; }
    }
}
