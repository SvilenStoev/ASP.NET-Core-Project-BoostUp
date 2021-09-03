namespace BoostUp.Services.Companies
{
    using System.Collections.Generic;

    using Common;
    using BoostUp.Services.Companies.Models;

    public interface ICompanyService
    {
        CompanyQueryServiceModel All(
            string country = null,
            int industryId = 0,
            string searchTerm = null,
            CompanySorting sorting = CompanySorting.DateCreated,
            int currentPage = 1,
            int companiesPerPage = int.MaxValue,
            bool publicOnly = true);

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
            string websiteUrl,
            bool isPublic);

        CompanyDetailsServiceModel Details(int id);

        bool BecomeEmployee(string userId, int id);

        public string InformationById(int id);

        bool IsEmployee(string userId, int id);

        void Approve(int id);

        IEnumerable<string> AllCountries();

        IEnumerable<CompanyIndustryServiceModel> AllIndustries();

        IEnumerable<CompanyCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

        bool IndustryExists(int industryId);
    }
}
