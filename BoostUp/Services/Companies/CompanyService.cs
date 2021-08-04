namespace BoostUp.Services.Companies
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data;
    using BoostUp.Models.Companies;
    using BoostUp.Services.Companies.Models;
    using BoostUp.Data.Models;

    public class CompanyService : ICompanyService
    {
        private readonly BoostUpDbContext data;

        public CompanyService(BoostUpDbContext data)
            => this.data = data;

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
                    AddressCity = c.Address.City,
                    AddressCountry = c.Address.Country,
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
                AddressCountry = c.Address.Country,
                AddressCity = c.Address.City,
                AddressText = c.Address.AddressText,
                CategoryName = c.Category.Value,
                IndustryName = c.Industry.Value,
                JobsCount = c.Jobs.Count()
            })
            .ToList()
            .FirstOrDefault();

        public IEnumerable<string> AllCountries()
            => this.data
                .Companies
                .Select(c => c.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

        public IEnumerable<CompanyIndustryServiceModel> AllIndustries()
              => this.data
                  .Industries
                  .Select(i => new CompanyIndustryServiceModel
                  {
                      Id = i.Id,
                      Value = i.Value
                  })
                  .ToList();

        public IEnumerable<CompanyCategoryServiceModel> AllCategories()
               => this.data
                   .Categories
                   .Select(c => new CompanyCategoryServiceModel
                   {
                       Id = c.Id,
                       Value = c.Value
                   })
                   .ToList();

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
