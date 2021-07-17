namespace BoostUp.Controllers
{
    using BoostUp.Models.Companies;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddCompanyViewModel company)
        {

            return View(company);
        }
    }
}
