namespace BoostUp.Data
{
    using System;

    public class DataConstants
    {
        public const int CompanyNameMaxLength = 40;
        public const int CompanyMinYearFounded = 1900;
        public int CompanyMaxYearFounded = DateTime.UtcNow.Year;

        public const int JobTitleMaxLength = 60;

        public const int AddressCityMaxLength = 50;
        public const int AddressTextMaxLength = 100;
    }
}
