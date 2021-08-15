namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Infrastructure.Extensions;
    using BoostUp.Models.Addresses;
    using BoostUp.Models.Companies;
    using BoostUp.Services.Companies;
    using BoostUp.Services.Users;

    using static GlobalConstants;
    using Microsoft.Extensions.Caching.Memory;

    public class CompaniesController : Controller
    {
        private readonly ICompanyService companies;
        private readonly IUserService users;

        public CompaniesController(ICompanyService companies, IUserService users)
        {
            this.companies = companies;
            this.users = users;
        }

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

            TempData[GlobalMessageKey] = "Your company was added successfully and is awaiting for approval by the administrator!";

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

        [Authorize]
        public IActionResult Details(int id, string information)
        {
            var company = this.companies.Details(id);

            if (company == null || company.CompanyInformation() != information)
            {
                TempData[GlobalMessageKey] = "Oops... The company does not exist!";

                return RedirectToAction(nameof(All));
            }

            var userId = this.User.GetId();

            company.UserFirstName = this.users.FirstNameById(userId);
            company.UserIsEmployed = this.users.IsEmployed(userId);

            return View(company);
        }

        [Authorize]
        public IActionResult BecomeEmployee(CompanyBecomeEmployeeViewModel becomeEmployee)
        {
            return View(becomeEmployee);
        }

        [HttpPost]
        [Authorize]
        public IActionResult BecomeEmployee(int id)
        {
            var userId = this.User.GetId();

            this.companies.BecomeEmployee(userId, id);

            TempData[GlobalMessageKey] = "Your workplace was changed successfully!";

            var information = this.companies.InformationById(id);

            return RedirectToAction(nameof(Details), new { id, information });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var company = this.companies.Details(id);

            return View(new CompanyInputModel
            {
                Name = company.Name,
                Founded = company.Founded,
                Overview = company.Overview,
                LogoUrl = company.LogoUrl,
                WebsiteUrl = company.WebsiteUrl,
                Address = new AddressInputModel
                {
                    Country = company.Country,
                    City = company.City,
                    AddressText = company.AddressText,
                },
                IndustryId = company.IndustryId,
                Industries = this.companies.AllIndustries(),
                CategoryId = company.CategoryId,
                Categories = this.companies.AllCategories(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CompanyInputModel company)
        {
            var userIsAdmin = this.User.IsAdmin();

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            if (!this.companies.CategoryExists(company.CategoryId))
            {
                this.ModelState.AddModelError(nameof(company.CategoryId), "Category does not exist.");
            }

            if (!this.companies.IndustryExists(company.IndustryId))
            {
                this.ModelState.AddModelError(nameof(company.IndustryId), "Industry does not exist.");
            }

            if (!ModelState.IsValid)
            {
                company.Industries = this.companies.AllIndustries();
                company.Categories = this.companies.AllCategories();

                return View(company);
            }

            this.companies.Edit(
                id,
                company.Name,
                company.Founded,
                company.Overview,
                company.IndustryId,
                company.CategoryId,
                company.Address.Country,
                company.Address.City,
                company.Address.AddressText,
                company.LogoUrl,
                company.WebsiteUrl,
                userIsAdmin);

            TempData[GlobalMessageKey] = "The company was edited successfully!";

            var information = company.CompanyInformation();

            return RedirectToAction(nameof(Details), new { id, information });
        }
    }
}
