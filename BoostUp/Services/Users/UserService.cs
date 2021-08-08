namespace BoostUp.Services.Users
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BoostUp.Data;
    using BoostUp.Services.Users.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly BoostUpDbContext data;
        private readonly IMapper mapper;

        public UserService(BoostUpDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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

        public ProfileServiceModel Details(string id)
            => this.data
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<ProfileServiceModel>(this.mapper.ConfigurationProvider)
                .ToList()
                .FirstOrDefault();
    }
}
