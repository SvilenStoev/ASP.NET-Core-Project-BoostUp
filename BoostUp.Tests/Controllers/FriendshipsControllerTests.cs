namespace BoostUp.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;

    public class FriendshipsControllerTests
    {
        private const string DefaultUserId = "TestUser";

        //Add Action

        [Fact]
        public void AddShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"/Friendships/Add?profileUserId={DefaultUserId}")
               .To<FriendshipsController>(c => c.Add(DefaultUserId));

        [Fact]
        public void AddShouldBeForAuthorizedUsers()
             => MyController<FriendshipsController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Add(DefaultUserId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void AddShouldReturnRedirectToAction()
             => MyController<FriendshipsController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Add(DefaultUserId))
                 .ShouldReturn()
                 .Redirect(redirect => redirect
                    .To<UsersController>(c => c.Profile(DefaultUserId)));

    }
}