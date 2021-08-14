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

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var jobs = Enumerable
                .Range(0, 10)
                .Select(i => new Job());

            data.Jobs.AddRange(jobs);

            data.SaveChanges();

            var jobService = new JobService(data, mapper);
            var userService = new UserService(data, mapper);
            var recruiterService = new RecruiterService(data);

            var jobsController = new JobsController(jobService, recruiterService, userService, mapper);

            //Act
            var result = jobsController.All(new JobsQueryModel());

            //Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var allViewModel = Assert.IsType<JobsQueryModel>(model);

            Assert.Equal(10, allViewModel.TotalJobs);
        }

        private static IEnumerable<Job> GetJobs()
      => Enumerable.Range(0, 10).Select(i => new Job());
    }
}
