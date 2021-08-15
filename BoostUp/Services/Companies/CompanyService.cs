namespace BoostUp.Services.Companies
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data;
    using BoostUp.Models.Companies;
    using BoostUp.Services.Companies.Models;
    using BoostUp.Data.Models;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Text;

    public class CompanyService : ICompanyService
    {
        private readonly BoostUpDbContext data;
        private readonly IMemoryCache cache;

        public CompanyService(BoostUpDbContext data, IMemoryCache cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public CompanyQueryServiceModel All(
            string country,
            int industryId,
            string searchTerm,
            CompanySorting sorting,
            int currentPage,
            int companiesPerPage)
        {
            var companiesQuery = this.data.Companies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(country))
            {
                companiesQuery = companiesQuery.Where(c => c.Address.Country == country);
            }

            if (industryId > 0)
            {
                companiesQuery = companiesQuery.Where(c => c.Industry.Id == industryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                companiesQuery = companiesQuery.Where(c =>
                    c.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Founded.ToString() == searchTerm);
            }

            companiesQuery = sorting switch
            {
                CompanySorting.DateCreated => companiesQuery.OrderByDescending(c => c.Id),
                CompanySorting.YearFounded => companiesQuery.OrderByDescending(c => c.Founded),
                CompanySorting.Name => companiesQuery.OrderBy(c => c.Name),
                CompanySorting.EmployeesCount => companiesQuery.OrderByDescending(c => c.Id), //TODO Employees count!!
                _ => companiesQuery.OrderByDescending(c => c.Id)
            };

            var companies = companiesQuery
                .Skip((currentPage - 1) * companiesPerPage)
                .Take(companiesPerPage)
                .Select(c => new CompanyServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Founded = c.Founded,
                    LogoUrl = c.LogoUrl,
                    City = c.Address.City,
                    Country = c.Address.Country,
                    CategoryName = c.Category.Value, //TODO: Show employees count
                    IndustryName = c.Industry.Value
                })
                .ToList();

            var totalCompanies = companiesQuery.Count();

            return new CompanyQueryServiceModel
            {
                Companies = companies,
                CurrentPage = currentPage,
                TotalCompanies = totalCompanies,
            };
        }

        public int Create(
               string name,
               int? founded,
               string overview,
               int industryId,
               int categoryId,
               string country,
               string city,
               string addressText,
               string logoUrl,
               string websiteUrl)
        {
            var companyToAdd = new Company
            {
                Name = name,
                Founded = founded,
                Overview = overview,
                IndustryId = industryId,
                CategoryId = categoryId,
                Address = new Address
                {
                    Country = country,
                    City = city,
                    AddressText = addressText
                },
                LogoUrl = logoUrl,
                WebsiteUrl = websiteUrl
            };

            this.data.Companies.Add(companyToAdd);

            this.data.SaveChanges();

            return companyToAdd.Id;
        }

        public bool Edit(
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
               string websiteUrl)
        {
            var company = this.data.Companies.Find(id);

            var companyAddress = this.data.Addresses.Find(company.AddressId);

            if (company == null)
            {
                return false;
            }

            company.Name = name;
            company.Founded = founded;
            company.Overview = overview;
            company.IndustryId = industryId;
            company.CategoryId = categoryId;
            companyAddress.Country = country;
            companyAddress.City = city;
            companyAddress.AddressText = addressText;
            company.LogoUrl = logoUrl;
            company.WebsiteUrl = websiteUrl;

            this.data.SaveChanges();

            return true;
        }

        public CompanyDetailsServiceModel Details(int id)
        => this.data
            .Companies
            .Where(c => c.Id == id)
            .Select(c => new CompanyDetailsServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                Founded = c.Founded,
                LogoUrl = c.LogoUrl,
                WebsiteUrl = c.WebsiteUrl,
                Overview = c.Overview,
                Country = c.Address.Country,
                City = c.Address.City,
                AddressText = c.Address.AddressText,
                CategoryId = c.CategoryId,
                CategoryName = c.Category.Value,
                IndustryId = c.IndustryId,
                IndustryName = c.Industry.Value,
                JobsCount = c.Jobs.Count(),
                EmployeesCount = c.Employees.Count(),
            })
            .ToList()
            .FirstOrDefault();

        public bool BecomeEmployee(string userId, int id)
        {
            var company = this.data.Companies.Find(id);
            var user = this.data.Users.Find(userId);

            if (company == null || user == null)
            {
                return false;
            }

            if (IsEmployee(userId, id))
            {
                return false;
            }

            user.CompanyId = id;

            this.data.SaveChanges();

            return true;
        }

        public string InformationById(int id)
        {
            var sb = new StringBuilder();

            var company = this.data.Companies.Find(id);

            sb.Append($"{company.Name}-{company.Founded}");

            return sb.ToString();
        }

        public bool IsEmployee(string userId, int id)
            => this.data
                .Users
                .Where(u => u.Id == userId)
                .Any(u => u.CompanyId == id);

        public IEnumerable<string> AllCountries()
            => this.data
                .Companies
                .Select(c => c.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

        public IEnumerable<CompanyIndustryServiceModel> AllIndustries()
        {
            const string allIndustriesCacheKey = "allIndustriesCacheKey";

            var industries = this.cache.Get<IEnumerable<CompanyIndustryServiceModel>>(allIndustriesCacheKey);

            if (industries == null)
            {
                industries = this.data
                    .Industries
                    .Select(i => new CompanyIndustryServiceModel
                    {
                        Id = i.Id,
                        Value = i.Value
                    })
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                this.cache.Set(allIndustriesCacheKey, industries, cacheOptions);
            }

            return industries;
        }

        public IEnumerable<CompanyCategoryServiceModel> AllCategories()
        {
            const string allCategoriesCacheKey = "allCategoriesCacheKey";

            var categories = this.cache.Get<IEnumerable<CompanyCategoryServiceModel>>(allCategoriesCacheKey);

            if (categories == null)
            {
                categories = this.data
                    .Categories
                    .Select(c => new CompanyCategoryServiceModel
                    {
                        Id = c.Id,
                        Value = c.Value
                    })
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                this.cache.Set(allCategoriesCacheKey, categories, cacheOptions);
            }

            return categories;
        }

        public bool IndustryExists(int industryId)
           => this.data
           .Industries
           .Any(i => i.Id == industryId);

        public bool CategoryExists(int categoryId)
          => this.data
          .Categories
          .Any(i => i.Id == categoryId);
    }
}
