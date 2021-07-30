namespace BoostUp.Services.Companies
{
    using System.Linq;
    using System.Collections.Generic;

    using BoostUp.Data;
    using BoostUp.Models.Companies;

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
                TotalCompanies = totalCompanies,
                CurrentPage = currentPage,
                Companies = companies,
            };
        }

        public IEnumerable<string> AllCompanyCountries()
            => this.data
                .Companies
                .Select(c => c.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

        public IEnumerable<CompanyIndustryServiceModel> AllCompanyIndustries()
          => this.data
          .Industries
          .Select(i => new CompanyIndustryServiceModel
          {
              Id = i.Id,
              Value = i.Value
          })
          .ToList();
    }
}
