﻿namespace BoostUp.Controllers
{
    using BoostUp.Services.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static BoostUp.GlobalConstants;

    public class AppliesController : Controller
    {
        private readonly IJobService jobs;

        public AppliesController(IJobService jobs) 
            => this.jobs = jobs;

        [Authorize]
        public IActionResult Add(int jobId)
        {
            var information = this.jobs.InformationById(jobId);

            if (information == null)
            {
                return Unauthorized();
            }

            TempData[GlobalMessageKey] = $"You have applied to the job '{information}' successfully!";

            return RedirectToAction("Details", "Jobs", new { id = jobId, information });
        }
    }
}