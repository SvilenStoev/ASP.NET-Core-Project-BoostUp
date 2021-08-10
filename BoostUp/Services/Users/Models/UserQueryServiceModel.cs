namespace BoostUp.Services.Users.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserQueryServiceModel
    {
        public int CurrentPage { get; init; } = 1;

        public int TotalUsers { get; set; }

        public string SearchTerm { get; init; }

        public IEnumerable<UsersServiceModel> Users { get; init; }
    }
}
