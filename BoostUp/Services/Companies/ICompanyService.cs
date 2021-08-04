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
            int IndustryId,
            int CategoryId,
            string country,
            string city,
            string addressText,
            string logoUrl,
            string WebsiteUrl);

        CompanyDetailsServiceModel Details(int id);

        IEnumerable<string> AllCountries();

        IEnumerable<CompanyIndustryServiceModel> AllIndustries();

        IEnumerable<CompanyCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

        bool IndustryExists(int industryId);
    }
}
