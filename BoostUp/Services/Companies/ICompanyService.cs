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

        int Create(
            string name,
            int? founded,
            string overview,
            int industryId,
            int categoryId,
            string country,
            string city,
            string addressText,
            string logoUrl,
            string websiteUrl);

        bool Edit(
            int id,
            string name,
            int? founded,
            string overview,
            int industryId,
            int categoryId,
            string country,
            string city,
            string addressText,
            string logoUrl,
            string websiteUrl);

        CompanyDetailsServiceModel Details(int id);

        bool BecomeEmployee(string userId, int id);

        bool IsEmployee(string userId, int id);

        IEnumerable<string> AllCountries();

        IEnumerable<CompanyIndustryServiceModel> AllIndustries();

        IEnumerable<CompanyCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

        bool IndustryExists(int industryId);
    }
}
