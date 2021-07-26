namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Infrastructure;
    using BoostUp.Models.Recruiters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class RecruitersController : Controller
    {
        private readonly BoostUpDbContext data;

        public RecruitersController(BoostUpDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(RecruiterInputModel recruiter)
        {
            if (UserIsRecruiter())
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(recruiter);
            }

            var recruiterToAdd = new Recruiter
            {
                UserId = this.User.GetId(),
                FirstName = recruiter.FirstName,
                LastName = recruiter.LastName,
                Email = recruiter.Email,
                PhoneNumber = recruiter.PhoneNumber
            };

            this.data.Recruiters.Add(recruiterToAdd);

            this.data.SaveChanges();

            return RedirectToAction("All", "Companies");
        }

        private bool UserIsRecruiter()
             => this.data.Recruiters.Any(r => r.UserId == this.User.GetId());
    }
}
