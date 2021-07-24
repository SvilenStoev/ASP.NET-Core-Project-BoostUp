namespace BoostUp.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;

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
                LogoUrl = company.LogoUrl,
                WebsiteUrl = company.WebsiteUrl
            };

            this.data.Companies.Add(companyToAdd);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]CompaniesQueryModel query)
        {
            var companiesQuery = this.data.Companies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Country))
            {
                companiesQuery = companiesQuery.Where(c => c.Address.Country == query.Country);
            }

            if (!string.IsNullOrWhiteSpace(query.Industry))
            {
                companiesQuery = companiesQuery.Where(c => c.Industry.Value.ToLower() == query.Industry);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                companiesQuery = companiesQuery.Where(c =>
                    c.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Founded.ToString() == query.SearchTerm);
            }

            companiesQuery = query.Sorting switch
            {
                CompanySorting.DateCreated => companiesQuery.OrderByDescending(c => c.Id),
                CompanySorting.YearFounded => companiesQuery.OrderByDescending(c => c.Founded),
                CompanySorting.Name => companiesQuery.OrderBy(c => c.Name),
                CompanySorting.EmployeesCount => companiesQuery.OrderByDescending(c => c.Id), //TODO Employees count!!
                _ => companiesQuery.OrderByDescending(c => c.Id)
            };

            var companies = companiesQuery
                .Skip((query.CurrentPage - 1) * CompaniesQueryModel.companiesPerPage)
                .Take(CompaniesQueryModel.companiesPerPage)
                .Select(c => new CompanyViewModel
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

            var companyCountries = this.data
                .Companies
                .Select(c => c.Address.Country)
                .OrderBy(c => c)
                .Distinct()
                .ToList();

            var companyIndustries = GetCompanyIndustries();
            var totalCompanies = companiesQuery.Count();

            query.Companies = companies;
            query.Countries = companyCountries;
            query.Industries = companyIndustries;
            query.TotalCompanies = totalCompanies;

            return View(query);
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
