﻿namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs;
    using BoostUp.Infrastructure;
    using BoostUp.Services.Recruiters;
    using AutoMapper;

    public class JobsController : Controller
    {
        private readonly IJobService jobs;
        private readonly IRecruiterService recruiters;
        private readonly IMapper mapper;

        public JobsController(IJobService jobs, IRecruiterService recruiters, IMapper mapper)
        {
            this.jobs = jobs;
            this.recruiters = recruiters;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add(int companyId)
        {
            if (!this.recruiters.IsRecruiter(this.User.GetId()))
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { companyId = companyId });
            }

            return View(new JobInputModel
            {
                EmploymentTypes = this.jobs.AllEmploymentTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(JobInputModel job)
        {
            var recruiterId = this.recruiters.IdByUser(this.User.GetId());

            if (recruiterId == null)
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { value = "companyId" });
            }

            if (!this.jobs.EmploymentTypeExists(job.EmploymentTypeId))
            {
                this.ModelState.AddModelError(nameof(job.EmploymentTypeId), "Employment type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                job.EmploymentTypes = this.jobs.AllEmploymentTypes();

                return View(job);
            }

            int jobId = this.jobs.Create(
                job.JobTitle,
                job.Address.Country,
                job.Address.City,
                job.Address.AddressText,
                job.Description,
                recruiterId,
                job.EmploymentTypeId,
                job.SalaryRangeFrom,
                job.SalaryRangeTo,
                job.CompanyId);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult All([FromQuery] JobsQueryModel query)
        {
            var jobsQuery = this.jobs.All(
                query.CompanyId,
                query.Country,
                query.EmploymentTypeId,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                JobsQueryModel.jobsPerPage);

            var jobCountries = this.jobs.AllCountries();
            var jobEmploymentTypes = this.jobs.AllEmploymentTypes();

            query.Jobs = jobsQuery.Jobs;
            query.TotalJobs = jobsQuery.TotalJobs;
            query.Countries = jobCountries;
            query.EmploymentTypes = jobEmploymentTypes;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            if (!this.recruiters.IsRecruiter(this.User.GetId()))
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters");
            }

            var userId = this.User.GetId();

            var myJobs = this.jobs.ByUser(userId);

            return View(myJobs);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.recruiters.IsRecruiter(userId) && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters");
            }

            var job = this.jobs.Details(id);

            if (job.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var jobInput = this.mapper.Map<JobInputModel>(job);

            jobInput.EmploymentTypes = this.jobs.AllEmploymentTypes();

            return View(jobInput);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, JobInputModel job)
        {
            var recruiterId = this.recruiters.IdByUser(this.User.GetId());

            if (recruiterId == null && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(RecruitersController.Become), "Recruiters", new { value = "companyId" });
            }

            if (!this.jobs.EmploymentTypeExists(job.EmploymentTypeId))
            {
                this.ModelState.AddModelError(nameof(job.EmploymentTypeId), "Employment type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                job.EmploymentTypes = this.jobs.AllEmploymentTypes();

                return View(job);
            }

            if (!this.jobs.IsByRecruiter(id, recruiterId) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            this.jobs.Edit(
                  id,
                  job.JobTitle,
                  job.Address.Country,
                  job.Address.City,
                  job.Address.AddressText,
                  job.Description,
                  job.EmploymentTypeId,
                  job.SalaryRangeFrom,
                  job.SalaryRangeTo,
                  job.CompanyId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Details() => View();
    }
}
