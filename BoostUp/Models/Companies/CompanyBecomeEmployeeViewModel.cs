namespace BoostUp.Models.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyBecomeEmployeeViewModel
    {
        public int companyId { get; init; }

        public string companyName { get; init; }

        public string userFirstName { get; init; }
    }
}
