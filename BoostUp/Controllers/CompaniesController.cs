namespace BoostUp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;

    public class CompaniesController : Controller
    {
        private readonly BoostUpDbContext data;

        public CompaniesController(BoostUpDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddCompanyViewModel
        {
            Categories = this.GetCompanyCategories(),
            Industries = this.GetCompanyIndustries()
        });

        [HttpPost]
        public IActionResult Add(AddCompanyViewModel company)
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
                LogoUrl = company.LogoUrl == null ? "https://www.market-research-companies.in//images/default.jpg" : company.LogoUrl,
                WebsiteUrl = company.WebsiteUrl
            };

            this.data.Companies.Add(companyToAdd);

            this.data.SaveChanges();

            return Redirect("/Home/Index");
        }

        private IEnumerable<CompanyIndustryViewModel> GetCompanyIndustries()
            => this.data
            .Industries
            .Select(i => new CompanyIndustryViewModel
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
