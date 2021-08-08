namespace BoostUp.Services.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool IsEmployed(string id);

        string FirstNameById(string id);
    }
}
