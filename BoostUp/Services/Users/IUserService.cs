namespace BoostUp.Services.Users
{
    using BoostUp.Services.Users.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService
    {
        UserQueryServiceModel All(string searchTerm,
                int currentPage,
                int usersPerPage,
                int companyId,
                string userId);

        ProfileServiceModel Details(string id);

        FriendshipServiceModel GetFriendship(string fromId, string toId);

        bool IsEmployed(string id);

        string FirstNameById(string id);
    }
}
