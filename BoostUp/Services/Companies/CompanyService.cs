namespace BoostUp.Services.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;
    using BoostUp.Services.Companies.Models;
    using Microsoft.Extensions.Caching.Memory;

    using static GlobalConstants.Cache;

    public class CompanyService : ICompanyService
    {
        private readonly BoostUpDbContext data;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;

        public CompanyService(BoostUpDbContext data, IMemoryCache cache, IMapper mapper)
        {
            this.data = data;
            this.cache = cache;
            this.mapper = mapper;
        }

        public CompanyQueryServiceModel All(
            string country = null,
            int industryId = 0,
            string searchTerm = null,
            CompanySorting sorting = CompanySorting.DateCreated,
            int currentPage = 1,
            int companiesPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var companiesQuery = this.data.Companies.Where(c => c.IsPublic == publicOnly).AsQueryable();

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
                CompanySorting.DateCreated =>  companiesQuery.OrderByDescending(c => c.Id),
                CompanySorting.YearFounded => companiesQuery.OrderByDescending(c => c.Founded),
                CompanySorting.Name => companiesQuery.OrderBy(c => c.Name),
                CompanySorting.EmployeesCount => companiesQuery.OrderByDescending(c => c.Employees.Count()),
                _ => companiesQuery.OrderByDescending(c => c.Id)
            };

            var companies = companiesQuery
                .Skip((currentPage - 1) * companiesPerPage)
                .Take(companiesPerPage)
                .ProjectTo<CompanyServiceModel>(this.mapper.ConfigurationProvider)
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
                WebsiteUrl = websiteUrl,
                IsPublic = false
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
               string websiteUrl,
               bool isPublic)
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
            company.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public CompanyDetailsServiceModel Details(int id)
        => this.data
            .Companies
            .Where(c => c.Id == id)
            .ProjectTo<CompanyDetailsServiceModel>(this.mapper.ConfigurationProvider)
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

        public void Approve(int id)
        {
            var company = this.data.Companies.Find(id);

            company.IsPublic = true;

            this.data.SaveChanges();
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
            var industries = this.cache.Get<IEnumerable<CompanyIndustryServiceModel>>(allIndustriesCacheKey);

            if (industries == null)
            {
                industries = this.data
                    .Industries
                    .ProjectTo<CompanyIndustryServiceModel>(this.mapper.ConfigurationProvider)
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                this.cache.Set(allIndustriesCacheKey, industries, cacheOptions);
            }

            return industries;
        }

        public IEnumerable<CompanyCategoryServiceModel> AllCategories()
        {
            var categories = this.cache.Get<IEnumerable<CompanyCategoryServiceModel>>(allCategoriesCacheKey);

            if (categories == null)
            {
                categories = this.data
                    .Categories
                    .ProjectTo<CompanyCategoryServiceModel>(this.mapper.ConfigurationProvider)
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
