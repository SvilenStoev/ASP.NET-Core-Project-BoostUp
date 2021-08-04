namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Infrastructure;
    using BoostUp.Models.Recruiters;
    using BoostUp.Services.Recruiters;

    public class RecruitersController : Controller
    {
        private readonly IRecruiterService recruiters;

        public RecruitersController(IRecruiterService recruiters) 
            => this.recruiters = recruiters;

        [Authorize]
        public IActionResult Become()
        {
            if (this.recruiters.IsRecruiter(this.User.GetId()))
            {
                return BadRequest();
            }

            return View();
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
                recruiter.FirstName,
                recruiter.LastName,
                recruiter.Email,
                recruiter.PhoneNumber);

            return RedirectToAction("All", "Companies");
        }
    }
}
