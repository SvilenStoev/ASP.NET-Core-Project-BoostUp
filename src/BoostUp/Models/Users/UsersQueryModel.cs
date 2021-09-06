namespace BoostUp.Models.Users
{
    using BoostUp.Services.Users.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersQueryModel
    {
        public const int usersPerPage = 12;

        public int CurrentPage { get; init; } = 1;

        public int CompanyId { get; set; }

        public int TotalUsers { get; set; }

        [Display(Name = "Search by name")]
        public string SearchTerm { get; init; }

        public IEnumerable<UsersServiceModel> Users { get; set; } 
    }
}
