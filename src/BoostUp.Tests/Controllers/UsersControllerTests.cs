namespace BoostUp.Tests.Controllers
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;
    using BoostUp.Models.Users;

    using static Data.Users;

    public class UsersControllerTests
    {
        private const string DefaultUserId = "TestUser";

        //Profile Action

        [Fact]
        public void ProfileShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap($"/Users/Profile?id={DefaultUserId}")
           .To<UsersController>(c => c.Profile(DefaultUserId));

        [Fact]
        public void ProfileShouldBeForAuthorizedUsers()
             => MyController<UsersController>
                 .Instance()
                 .Calling(c => c.Profile(DefaultUserId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        //[Fact]
        //public void ProfileShouldReturnViewWithCorrectModelAndData()
        //  => MyController<UsersController>
        //         .Instance(controller => controller
        //            .WithData(new User
        //            {
        //                Id = DefaultUserId
        //            }))
        //        .Calling(c => c.Profile(DefaultUserId))
        //        .ShouldReturn()
        //        .View(view => view
        //            .WithModelOfType<ProfileServiceModel>());

        [Fact]
        public void ProfileShouldReturnBadRequestIfUserDoestNotExists()
          => MyController<UsersController>
                 .Instance()
                .Calling(c => c.Profile(DefaultUserId))
                .ShouldReturn()
                .Unauthorized();

        //All Action

        [Fact]
        public void AllShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap($"/Users/All")
           .To<UsersController>(c => c.All(new UsersQueryModel()));

        [Fact]
        public void AllShouldReturnViewWithCorrectModelAndData()
          => MyController<UsersController>
            .Instance(controller => controller
                 .WithUser(TestUser.Identifier)
                 .WithData(FiveUsers()))
            .Calling(c => c.All(new UsersQueryModel()))
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<UsersQueryModel>()
                .Passing(m => m.TotalUsers.Should().Be(5)));
    }
}
