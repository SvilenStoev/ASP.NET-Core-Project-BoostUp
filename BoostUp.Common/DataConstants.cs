namespace BoostUp.Common
{
    public class DataConstants
    {
        //Global
        public class Address
        {
            public const int CityMinLength = 2;
            public const int CityMaxLength = 25;
            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 20;
            public const int TextMinLength = 5;
            public const int TextMaxLength = 100;
        }

        public class Post
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 400;
        }

        public class Comment
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 200;
        }

        //Users related
        public class User
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int JobTitleMaxLength = 30;
            public const int AboutMaxLength = 600;
            public const int ExperienceMaxLength = 600;
            public const int EducationMaxLength = 600;
        }

        public class Recruiter
        {
            public const int PhoneNumberMaxLength = 15;
            public const int PhoneNumberMinLength = 3;
        }

        //Companies related
        public class Company
        {
            public const string DefaultCompanyLogoPath = "~/default pictures/Company Logo.jpg";

            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
            public const int MinYearFounded = 1800;
            public const int MaxYearFounded = 2100;
            public const int OverviewMinLength = 30;
        }

        public class Industry
        {
            public const int ValueMaxLength = 25;
        }

        public class Category
        {
            public const int ValueMaxLength = 25;
        }

        //Jobs related
        public class Job
        {
            public const string SalaryRangeMessage = "Salary range must be between {1} and {2}.";

            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 40;
            public const int DescriptionMinLength = 30;
            public const int SalaryMinRange = 200;
            public const int SalaryMaxRange = 100000;
        }

        public class EmploymentType
        {
            public const int ValueMaxLength = 25;
        }
    }
}
