namespace BoostUp.Services.Jobs
{
    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs.Models;
    using System.Collections.Generic;

    public interface IJobService
    {
        JobQueryServiceModel All(
            int companyId,
            string Country,
            int EmploymentTypeId,
            string SearchTerm,
            JobSorting sorting,
            int currentPage,
            int companiesPerPage);

        int Create(
            string jobTitle,
            string country,
            string city,
            string addressText,
            string description,
            string recruiterId,
            int employmentTypeId,
            int? salaryRangeFrom,
            int? salaryRangeTo,
            int companyId);

        bool Edit(
            int jobId,
            string jobTitle,
            string country,
            string city,
            string addressText,
            string description,
            int employmentTypeId,
            int? salaryRangeFrom,
            int? salaryRangeTo,
            int companyId);

        JobDetailsServiceModel Details(int jobId);

        IEnumerable<JobServiceModel> ByUser(string userId);

        IEnumerable<string> AllCountries();

        IEnumerable<JobEmploymentTypeServiceModel> AllEmploymentTypes();

        bool IsByRecruiter(int jobId, string userId);

        bool EmploymentTypeExists(int employmentTypeId);
    }
}
