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

        public CompaniesController(ICompanyService companies) 
            => this.companies = companies;

        [Authorize]
        public IActionResult Add()
        {
            return View(new CompanyInputModel
            {
                Categories = this.companies.AllCategories(),
                Industries = this.companies.AllIndustries()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CompanyInputModel company)
        {
            if (!this.companies.CategoryExists(company.CategoryId))
            {
                this.ModelState.AddModelError(nameof(company.CategoryId), "Category does not exist.");
            }
            else if (!this.companies.IndustryExists(company.IndustryId))
            {
                this.ModelState.AddModelError(nameof(company.IndustryId), "Industry does not exist.");
            }

            if (!ModelState.IsValid)
            {
                company.Categories = this.companies.AllCategories();
                company.Industries = this.companies.AllIndustries();

                return View(company);
            }

            int companyId = this.companies.Create(
                company.Name,
                company.Founded,
                company.Overview,
                company.IndustryId,
                company.CategoryId,
                company.Address.Country,
                company.Address.City,
                company.Address.AddressText,
                company.LogoUrl,
                company.WebsiteUrl);

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

            var companyCountries = this.companies.AllCountries();
            var companyIndustries = this.companies.AllIndustries();

            query.TotalCompanies = companiesQuery.TotalCompanies;
            query.Companies = companiesQuery.Companies;
            query.Industries = companyIndustries;
            query.Countries = companyCountries;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var company = this.companies.Details(id);

            if (company == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(company);
        }
    }
}
