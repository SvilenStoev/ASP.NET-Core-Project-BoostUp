namespace BoostUp.Controllers
{
    using BoostUp.Infrastructure;
    using BoostUp.Models.Users;
    using BoostUp.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        [Authorize]
        public IActionResult Profile(string id)
        {
            var user = this.users.Details(id);
            user.CurrentLoggedUser = User.GetId();

            user.FriendShip = this.users.GetFriendship(user.CurrentLoggedUser, id);

            if (user.FriendShip == null)
            {
                this.RedirectToAction(nameof(All));
            }

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
