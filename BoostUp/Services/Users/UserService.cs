namespace BoostUp.Services.Users
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Services.Users.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static BoostUp.Areas.Admin.AdminConstants;

    public class UserService : IUserService
    {
        private readonly BoostUpDbContext data;
        private readonly IMapper mapper;

        public UserService(BoostUpDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public UserQueryServiceModel All(string searchTerm,
            int currentPage,
            int usersPerPage,
            int companyId,
            string userId)
        {
            var usersQuery = this.data.Users.AsQueryable();

            if (companyId > 0)
            {
                usersQuery = usersQuery.Where(u => u.CompanyId == companyId);
            }
            else
            {
                usersQuery = this.data.Users.Where(u => u.Id != userId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                usersQuery = usersQuery.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(searchTerm.ToLower()));
            }

            var users = usersQuery
                .Skip(usersPerPage * (currentPage - 1))
                .Take(usersPerPage)
                .Select(u => new UsersServiceModel
                {
                    Id = u.Id,
                    FullName = u.FirstName + " " + u.LastName,
                    JobTitle = u.JobTitle,
                    City = u.Address.City,
                    CompanyName = u.Company.Name,
                    Country = u.Address.Country,
                    ProfileImagePath = GlobalConstants.GetProfileImagePath(u.ProfileImageId, u.ProfileImage.Extension, u.ProfileImage.ImageUrl, u.Gender)
                })
                .ToList();

            var totalUsers = usersQuery.Count();

            return new UserQueryServiceModel
            {
                Users = users,
                TotalUsers = totalUsers,
                CurrentPage = currentPage,
            };
        }

        public ProfileServiceModel Details(string id)
            => this.data
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<ProfileServiceModel>(this.mapper.ConfigurationProvider)
                .ToList()
                .FirstOrDefault();

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
