namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            if (data.Users.Any())
            {
                return;
            }

            var userManager = services.GetRequiredService<UserManager<User>>();

            Task
                .Run(async () =>
                {
                    const string userEmail = "SeededUser@boostup.com";
                    const string userPassword = "test123";

                    var user = new User
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FirstName = "Svilen",
                        LastName = "Stoev",
                        Gender = GenderType.Male,
                        DateOfBirth = new DateTime(1993, 12, 6),
                        ProfileImage = new Image
                        {
                            ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D03AQGAIpSGJ_scXw/profile-displayphoto-shrink_400_400/0/1546704920626?e=1634774400&v=beta&t=G87G_k-BzLOPl2tz5AznnPrkFbzHJNSKT5l1b6l1LfI",
                        },
                        Address = new Address
                        {
                            Country = "Bulgaria",
                            City = "Sofia",
                            AddressText = "Gotse Delchev 108A",
                        }
                    };

                    var result = await userManager.CreateAsync(user, userPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
