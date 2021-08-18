namespace BoostUp.Tests.Controllers
{
    using System.Linq;
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;
    using BoostUp.Data.Models;
    using BoostUp.Models.Jobs;

    using static Data.Jobs;
    using static Data.EmploymentTypes;
    using static GlobalConstants;
    using BoostUp.Models.Addresses;

    public class JobsControllerTests
    {
        private const int DefaultCompanyId = 1;
        private const string TestJobDescription = "Blue And Nice Test Job in company with the best oportunities";

        //Add Action

        [Fact]
        public void AddShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap($"/Jobs/Add?companyId={DefaultCompanyId}")
           .To<JobsController>(c => c.Add(DefaultCompanyId));

        [Fact]
        public void GetAddShouldBeForAuthorizedUsers()
             => MyController<JobsController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Add(DefaultCompanyId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetAddShouldReturnViewWithValidModel()
             => MyController<JobsController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Recruiter
                    {
                        UserId = TestUser.Identifier
                    })
                    .WithData(FiveEmploymentTypes()))
                 .Calling(c => c.Add(DefaultCompanyId))
                 .ShouldReturn()
                 .View(view => view
                     .WithModelOfType<JobInputModel>()
                     .Passing(model => model.EmploymentTypes.Should().HaveCount(5)));

        [Fact]
        public void GetAddShouldRedirectWhenUserIsNotRecruiter()
            => MyController<JobsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(DefaultCompanyId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RecruitersController>(c => c.Become(DefaultCompanyId)));

        [Fact]
        public void PostAddShouldBeForAuthorizedUsers()
         => MyController<JobsController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.Add(new JobInputModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData("RecruiterId", 2, "Accountant", TestJobDescription, "Bulgaria", "Sofia")]
        public void PostAddShouldReturnRedirectWithValidModelAndQueryString(
            string recruiterId,
            int employmentTypeId,
            string jobTitle,
            string description,
            string country,
            string city)
            => MyController<JobsController>
                .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Recruiter
                    {
                        Id = recruiterId,
                        UserId = TestUser.Identifier
                    })
                    .WithData(new EmploymentType
                    {
                        Id = employmentTypeId
                    }))
                .Calling(c => c.Add(new JobInputModel
                {
                    JobTitle = jobTitle,
                    Description = description,
                    CompanyId = DefaultCompanyId,
                    EmploymentTypeId = employmentTypeId,
                    Address = new AddressInputModel
                    {
                        Country = country,
                        City = city,
                    }
                }))
                .ShouldHave()
                .ValidModelState()
                .Data(data => data
                    .WithSet<Job>(jobs => jobs
                        .Any(c =>
                            c.JobTitle == jobTitle &&
                            c.Description == description)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<JobsController>(c => c.Details(1, jobTitle)));

        //All Action

        [Fact]
        public void AllShouldReturnViewWithCorrectModelAndData()
      => MyMvc
        .Pipeline()
        .ShouldMap("/Jobs/All")
        .To<JobsController>(j => j.All(new JobsQueryModel()))
        .Which(controller => controller
            .WithData(FiveJobs()))
        .ShouldReturn()
        .View(view => view
            .WithModelOfType<JobsQueryModel>()
            .Passing(m => m.TotalJobs.Should().Be(5)));
    }
}
