namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using BoostUp.Models.Companies;
    using BoostUp.Data;
    using System.Linq;

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



            return View(company);
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
