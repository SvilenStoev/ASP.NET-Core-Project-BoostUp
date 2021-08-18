namespace BoostUp.Controllers
{
    using BoostUp.Infrastructure.Extensions;
    using BoostUp.Models.Users;
    using BoostUp.Services.Friendships;
    using BoostUp.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IFriendshipService friendships;

        public UsersController(IUserService users, IFriendshipService friendships)
        {
            this.users = users;
            this.friendships = friendships;
        }

        [Authorize]
        public IActionResult Profile(string id)
        {
            var user = this.users.Details(id);

            if (user == null)
            {
                return Unauthorized();
            }

            user.CurrentLoggedUser = User.GetId();

            user.FriendShip = this.friendships.GetFriendship(user.CurrentLoggedUser, id);

            return View(user);
        }

        [Authorize]
        public IActionResult All([FromQuery] UsersQueryModel query)
        {
            var userId = User.GetId();

            var usersQuery = this.users.All(
                query.SearchTerm,
                query.CurrentPage,
                UsersQueryModel.usersPerPage,
                query.CompanyId,
                userId);    

            query.Users = usersQuery.Users;
            query.TotalUsers = usersQuery.TotalUsers;

            return View(query);
        }
    }
}
