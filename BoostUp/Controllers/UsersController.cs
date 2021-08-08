namespace BoostUp.Controllers
{
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

            return View(user);
        }


    }
}
