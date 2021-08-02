namespace BoostUp.Services.Companies
{
    using System.Collections.Generic;

    using BoostUp.Models.Companies;
    using BoostUp.Services.Companies.Models;

    public interface ICompanyService
    {
        CompanyQueryServiceModel All(
            string country,
            int industryId,
            string searchTerm,
            CompanySorting sorting,
            int currentPage,
            int companiesPerPage);

        IEnumerable<string> AllCompanyCountries();

        IEnumerable<CompanyIndustryServiceModel> AllCompanyIndustries();
    }
}
