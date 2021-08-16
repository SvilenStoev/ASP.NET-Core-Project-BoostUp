namespace BoostUp.Infrastructure.Extensions
{
    using BoostUp.Services.Companies.Models;
    using BoostUp.Services.Jobs.Models;

    public static class ModelExtensions
    {
        public static string CompanyInformation(this ICompanyModel company)
            => $"{company.Name}-{company.Founded}";

        public static string JobInformation(this IJobModel job)
       => $"{job.JobTitle}";
    }
}
