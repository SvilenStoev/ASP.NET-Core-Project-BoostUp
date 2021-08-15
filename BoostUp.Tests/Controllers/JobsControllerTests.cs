namespace BoostUp.Tests.Controllers
{
    using BoostUp.Controllers;
    using BoostUp.Data.Models;
    using BoostUp.Models.Jobs;
    using BoostUp.Services.Jobs;
    using BoostUp.Services.Recruiters;
    using BoostUp.Services.Users;
    using BoostUp.Tests.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    public class JobsControllerTests
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModelAndData()
       => MyMvc
           .Pipeline()
           .ShouldMap("/Jobs/All")
           .To<JobsController>(j => j.All(new JobsQueryModel()))
           .Which(controller => controller
               .WithData(GetJobs()))
           .ShouldReturn()
           .View(view => view
               .WithModelOfType<JobsQueryModel>()
               .Passing(m => m.TotalJobs.Should().Be(10)));

        private static IEnumerable<Job> GetJobs()
      => Enumerable.Range(0, 10).Select(i => new Job());
    }
}
