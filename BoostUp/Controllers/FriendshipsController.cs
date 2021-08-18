namespace BoostUp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BoostUp.Services.Friendships;
    using BoostUp.Infrastructure.Extensions;

    public class FriendshipsController : Controller
    {
        private readonly IFriendshipService friendships;

        public FriendshipsController(IFriendshipService friendships)
        {
            this.friendships = friendships;
        }

        [Authorize]
        public IActionResult Add(string profileUserId)
        {
            var currentUserId = this.User.GetId();

            this.friendships.AddFriendship(currentUserId, profileUserId);

            return RedirectToAction("Profile", "Users", new { id = profileUserId });
        }
    }
}
