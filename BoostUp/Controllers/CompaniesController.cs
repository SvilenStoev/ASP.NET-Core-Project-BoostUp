namespace BoostUp.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;

    using static Data.DataConstants;

    public class CompaniesController : Controller
    {
        private readonly BoostUpDbContext data;

        public CompaniesController(BoostUpDbContext data) => this.data = data;

        public IActionResult Add() => View(new CompanyInputModel
        {
            Categories = this.GetCompanyCategories(),
            Industries = this.GetCompanyIndustries()
        });

        [HttpPost]
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
                LogoUrl = company.LogoUrl == null ? $"{DefaultCompanyLogoPath}" : company.LogoUrl,
                WebsiteUrl = company.WebsiteUrl
            };

            this.data.Companies.Add(companyToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
        

        public IActionResult All()
        {
            var companies = this.data
                .Companies
                .OrderByDescending(c => c.Id)
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Founded = c.Founded,
                    LogoUrl = c.LogoUrl,
                    WebsiteUrl = c.WebsiteUrl,
                    AddressCity = c.Address.City,
                    AddressCountry = c.Address.Country,
                    AddressText = c.Address.AddressText,
                    CategoryName = c.Category.Value,
                    IndustryName = c.Industry.Value
                })
                .ToList();

            return View(companies);
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
