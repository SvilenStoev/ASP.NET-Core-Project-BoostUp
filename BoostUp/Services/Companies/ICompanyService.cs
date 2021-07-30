using BoostUp.Models.Companies;
using System.Collections.Generic;

namespace BoostUp.Services.Companies
{
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
