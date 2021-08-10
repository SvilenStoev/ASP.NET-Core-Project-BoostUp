namespace BoostUp.Services.Users.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersServiceModel
    {
        public string Id { get; init; }

        public string FullName { get; init; }

        public string JobTitle { get; init; }

        public string CompanyName { get; init; }

        public string City { get; init; }

        public string Country { get; init; }

        public string ProfileImagePath { get; set; }
    }
}
