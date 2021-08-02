namespace BoostUp.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;
    using Microsoft.AspNetCore.Authorization;
    using BoostUp.Services.Companies;
    using BoostUp.Services.Companies.Models;

    public class CompaniesController : Controller
    {
        private readonly ICompanyService companies;
        private readonly BoostUpDbContext data;

        public CompaniesController(ICompanyService companies, BoostUpDbContext data)
        {
            this.companies = companies;
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new CompanyInputModel
            {
                Categories = this.GetCompanyCategories(),
                Industries = this.GetCompanyIndustries()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CompanyInputModel company)
        {
            if (!this.data.Categories.Any(c => c.Id == company.CategoryId))
            {
                this.ModelState.AddModelError(nameof(company.CategoryId), "Category does not exist.");
            }
            else if (!this.data.Industries.Any(i => i.Id == company.IndustryId))
            {
                this.ModelState.AddModelError(nameof(company.IndustryId), "Industry does not exist.");
            }

            if (!ModelState.IsValid)
            {
                company.Categories = this.GetCompanyCategories();
                company.Industries = this.GetCompanyIndustries();

                return View(company);
            }

            var companyToAdd = new Company
            {
                Name = company.Name,
                Founded = company.Founded,
                Overview = company.Overview,
                IndustryId = company.IndustryId,
                CategoryId = company.CategoryId,
                Address = new Address
                {
                    Country = company.Address.Country,
                    City = company.Address.City,
                    AddressText = company.Address.AddressText
                },
                LogoUrl = company.LogoUrl,
                WebsiteUrl = company.WebsiteUrl
            };

            this.data.Companies.Add(companyToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] CompaniesQueryModel query)
        {
            var companiesQuery = this.companies.All(
                query.Country,
                query.IndustryId,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                CompaniesQueryModel.companiesPerPage);

            var companyCountries = this.companies.AllCompanyCountries();
            var companyIndustries = this.companies.AllCompanyIndustries();

            query.TotalCompanies = companiesQuery.TotalCompanies;
            query.Companies = companiesQuery.Companies;
            query.Industries = companyIndustries;
            query.Countries = companyCountries;

            return View(query);
        }

        public IActionResult Details(int companyId)
        {
            var company = this.data
                .Companies
                .Where(c => c.Id == companyId)
                .Select(c => new CompanyDetailsViewModel
                {
                    CompanyId = companyId,
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
                .FirstOrDefault();

            if (company == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(company);
        }

        private IEnumerable<CompanyIndustryServiceModel> GetCompanyIndustries()
            => this.data
            .Industries
            .Select(i => new CompanyIndustryServiceModel
            {
                Id = i.Id,
                Value = i.Value
            })
            .ToList();

        private IEnumerable<CompanyCategoryViewModel> GetCompanyCategories()
           => this.data
           .Categories
           .Select(i => new CompanyCategoryViewModel
           {
               Id = i.Id,
               Value = i.Value
           })
           .ToList();
    }
}
