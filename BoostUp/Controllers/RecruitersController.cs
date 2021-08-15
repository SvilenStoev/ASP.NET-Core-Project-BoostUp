﻿namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Infrastructure;
    using BoostUp.Models.Recruiters;
    using BoostUp.Services.Recruiters;

    using static GlobalConstants;
    using BoostUp.Services.Companies;

    public class RecruitersController : Controller
    {
        private readonly IRecruiterService recruiters;
        private readonly ICompanyService companies;

        public RecruitersController(IRecruiterService recruiters, ICompanyService companies)
        {
            this.recruiters = recruiters;
            this.companies = companies;
        }

        [Authorize]
        public IActionResult Become(int companyId)
        {
            if (this.recruiters.IsRecruiter(this.User.GetId()))
            {
                return BadRequest();
            }

            return View(new RecruiterInputModel { CompanyId = companyId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(RecruiterInputModel recruiter)
        {
            if (this.recruiters.IsRecruiter(this.User.GetId()))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(recruiter);
            }

            var userId = this.User.GetId();

            this.recruiters.Create(
                userId,
                recruiter.Email,
                recruiter.PhoneNumber);

            var companyId = recruiter.CompanyId;
            
            if (companyId != 0)
            {
                TempData[GlobalMessageKey] = "You have become recruiter! Now you can post jobs to the company.";

                var information = this.companies.InformationById(companyId);

                return RedirectToAction("Details", "Companies", new { id = companyId, information });
            }
            else
            {
                TempData[GlobalMessageKey] = "You have become recruiter! Now you can post jobs to any company through its details.";

                return RedirectToAction("All", "Companies");
            }
        }
    }
}
