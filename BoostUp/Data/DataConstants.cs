namespace BoostUp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataConstants
    {
        public const int CompanyNameMaxLength = 40;

        public const int CompanyMinYearFounded = 1900;
        public int CompanyMaxYearFounded = DateTime.UtcNow.Year;

        public const int JobTitleMaxLength = 60;


    }
}
