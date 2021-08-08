namespace BoostUp.Services.Users
{
    using BoostUp.Services.Users.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool IsEmployed(string id);

        string FirstNameById(string id);

        ProfileServiceModel Details(string id);
    }
}
