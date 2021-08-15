namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Infrastructure;
    using BoostUp.Models.Recruiters;
    using BoostUp.Services.Recruiters;

    using static GlobalConstants;

    public class RecruitersController : Controller
    {
        private readonly IRecruiterService recruiters;

        public RecruitersController(IRecruiterService recruiters) 
            => this.recruiters = recruiters;

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

            TempData[GlobalMessageKey] = "You have become recruiter. You can post jobs now.";

            var companyId = recruiter.CompanyId;

            if (companyId != 0)
            {
                return RedirectToAction("Details", "Companies", new { id = companyId });
            }
            else
            {
                return RedirectToAction("All", "Companies");
            }
        }
    }
}
