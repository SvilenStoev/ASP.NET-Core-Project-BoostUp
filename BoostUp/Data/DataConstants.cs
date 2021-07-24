namespace BoostUp.Data
{
    public class DataConstants
    {
        public class Company
        {
            public const string DefaultCompanyLogoPath = "~/default pictures/Company Logo.jpg";

            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
            public const int MinYearFounded = 1900;
            public const int MaxYearFounded = 2100;
            public const int MinOverviewLength = 30;
        }

        public class Address
        {
            public const int CityMinLength = 2;
            public const int CityMaxLength = 25;
            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 20;
            public const int TextMinLength = 5;
            public const int TextMaxLength = 100;
        }

        public class Industry
        {
            public const int ValueMaxLength = 25;
        }

        public class Job
        {
            public const int TitleMaxLength = 60;
        }
    }
}
