namespace BoostUp.Services.Users
{
    using BoostUp.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly BoostUpDbContext data;

        public UserService(BoostUpDbContext data)
            => this.data = data;

        public bool IsEmployed(string id)
            => this.data
            .Users
            .Any(u => u.Id == id && u.CompanyId != null);

        public string FirstNameById(string id)
            => this.data
                .Users
                .Where(u => u.Id == id)
                .Select(u => u.FirstName)
                .FirstOrDefault();
    }
}
