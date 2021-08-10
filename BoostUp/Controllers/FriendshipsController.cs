namespace BoostUp.Controllers
{
    using BoostUp.Infrastructure;
    using BoostUp.Services.Friendships;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
