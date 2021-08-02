namespace BoostUp.Services.Jobs
{
    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs.Models;
    using System.Collections.Generic;

    public interface IJobService
    {
        public JobQueryServiceModel All(
            int companyId,
            string Country,
            int EmploymentTypeId,
            string SearchTerm,
            JobSorting sorting,
            int currentPage,
            int companiesPerPage);

        public IEnumerable<string> AllJobCountries();

        public IEnumerable<JobEmploymentTypeServiceModel> AllEmploymentTypes();
    }
}
