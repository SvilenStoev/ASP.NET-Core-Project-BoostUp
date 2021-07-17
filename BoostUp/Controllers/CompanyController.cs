namespace BoostUp.Controllers
{
    using BoostUp.Models.Company;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddCompanyViewModel company)
        {



            return View(company);
        }
    }
}
