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
    using BoostUp.Services.Jobs.Models;
    using System.Collections.Generic;
    using BoostUp.Tests.Mocks;

    public class JobsControllerTests
    {
        private const int DefaultCompanyId = 1;
        private const int DefaultJobId = 1;
        private const string DefaultRecruiterId = "RecruiterId";
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
        [InlineData(DefaultRecruiterId, 2, "Accountant", TestJobDescription, "Bulgaria", "Sofia")]
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

        //Mine Action 

        [Fact]
        public void MineShouldBeMapped()
         => MyRouting
         .Configuration()
         .ShouldMap("/Jobs/Mine")
         .To<JobsController>(c => c.Mine());

        [Fact]
        public void MineShouldBeForAuthorizedUsers()
         => MyController<JobsController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.Mine())
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests());

        [Fact]
        public void MineShouldReturnViewWithCorrectModelAndData()
          => MyController<JobsController>
              .Instance(controller => controller
                  .WithUser(TestUser.Identifier)
                  .WithData(new Job
                  {
                      RecruiterId = DefaultRecruiterId,
                      Recruiter = new Recruiter
                      {
                          Id = DefaultRecruiterId,
                          UserId = TestUser.Identifier,
                      },
                  }))
             .Calling(c => c.Mine())
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<IEnumerable<JobServiceModel>>());

        [Fact]
        public void MineShouldRedirectWhenUserIsNotRecruiter()
         => MyController<JobsController>
             .Instance(controller => controller
                 .WithUser(TestUser.Identifier))
            .Calling(c => c.Mine())
            .ShouldReturn()
            .BadRequest();

        //Edit Action

        [Fact]
        public void EditShouldBeMapped()
         => MyRouting
         .Configuration()
         .ShouldMap($"/Jobs/Edit?id={DefaultJobId}")
         .To<JobsController>(c => c.Edit(DefaultJobId));

        [Fact]
        public void GetEditShouldBeForAuthorizedUsers()
             => MyController<JobsController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Edit(DefaultJobId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        //[Fact]
        //public void GetEditShouldReturnViewWithValidModel()
        //     => MyMvc
        //        .Pipeline()
        //        .ShouldMap("/Jobs/Edit")
        //        .To<JobsController>(c => c.Edit(DefaultJobId))
        //        .Which(controller => controller
        //            .WithDependencies(JobServiceMock.Instance)
        //            .WithUser(TestUser.Identifier)
        //            .WithData(new Recruiter
        //            {
        //                UserId = TestUser.Identifier
        //            })
        //            .WithData(new Job
        //            {
        //                RecruiterId = DefaultRecruiterId,
        //                Recruiter = new Recruiter
        //                {
        //                    Id = DefaultRecruiterId,
        //                    UserId = TestUser.Identifier,
        //                },
        //            })
        //            .WithData(FiveEmploymentTypes()))
        //         .ShouldReturn()
        //         .View(view => view
        //             .WithModelOfType<JobInputModel>()
        //             .Passing(model => model.EmploymentTypes.Should().HaveCount(5)));

        [Fact]
        public void GetEditShouldRedirectWhenUserIsNotRecruiter()
            => MyController<JobsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Edit(0))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RecruitersController>(c => c.Become(0)));

        [Fact]
        public void PostEditShouldBeForAuthorizedUsers()
         => MyController<JobsController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.Edit(DefaultJobId, new JobInputModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests());

        //[Theory]
        //[InlineData(DefaultRecruiterId, 2, "Accountant", TestJobDescription, "Bulgaria", "Sofia")]
        //public void PostEditShouldReturnRedirectWithValidModelAndQueryString(
        //    string recruiterId,
        //    int employmentTypeId,
        //    string jobTitle,
        //    string description,
        //    string country,
        //    string city)
        //    => MyController<JobsController>
        //        .Instance(controller => controller
        //            .WithUser(TestUser.Identifier)
        //            .WithData(new Recruiter
        //            {
        //                Id = recruiterId,
        //                UserId = TestUser.Identifier
        //            })
        //            .WithData(new EmploymentType
        //            {
        //                Id = employmentTypeId
        //            }))
        //        .Calling(c => c.Edit(DefaultJobId, new JobInputModel
        //        {
        //            JobTitle = jobTitle,
        //            Description = description,
        //            CompanyId = DefaultCompanyId,
        //            EmploymentTypeId = employmentTypeId,
        //            Address = new AddressInputModel
        //            {
        //                Country = country,
        //                City = city,
        //            }
        //        }))
        //        .ShouldHave()
        //        .ValidModelState()
        //        .Data(data => data
        //            .WithSet<Job>(jobs => jobs
        //                .Any(c =>
        //                    c.JobTitle == jobTitle &&
        //                    c.Description == description)))
        //        .TempData(tempData => tempData
        //            .ContainingEntryWithKey(GlobalMessageKey))
        //        .AndAlso()
        //        .ShouldReturn()
        //        .Redirect(redirect => redirect
        //            .To<JobsController>(c => c.Details(1, jobTitle)));
    }
}
