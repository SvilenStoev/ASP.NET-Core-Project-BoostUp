namespace BoostUp.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;
    using BoostUp.Data.Models;

    public class AppliesControllerTests
    {
        private const int DefaultJobId = 1;
        private const string JobTitle = "Accountant";

        //Add Action

        [Fact]
        public void AddShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"/Applies/Add?jobId={DefaultJobId}")
               .To<AppliesController>(c => c.Add(DefaultJobId));

        [Fact]
        public void AddShouldBeForAuthorizedUsers()
             => MyController<AppliesController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Job
                    {
                        JobTitle = JobTitle
                    }))
                 .Calling(c => c.Add(DefaultJobId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void AddShouldReturnRedirectToAction()
             => MyController<AppliesController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Job
                    {
                        JobTitle = JobTitle
                    }))
                    .Calling(c => c.Add(DefaultJobId))
                    .ShouldReturn()
                    .Redirect(redirect => redirect
                       .To<JobsController>(c => c.Details(DefaultJobId, JobTitle)));
    }
}