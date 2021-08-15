namespace BoostUp.Areas.Admin.Controllers
{
    using BoostUp.Services.Companies;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : AdminController
    {
        private readonly ICompanyService companies;

        public CompaniesController(ICompanyService companies) => this.companies = companies;

        public IActionResult All()
        {
            var companies = this.companies
                .All(publicOnly: false)
                .Companies;

            return View(companies);
        }

        public IActionResult Approve(int id)
        {
            this.companies.Approve(id);

            return RedirectToAction(nameof(All));
        }
    }
}
