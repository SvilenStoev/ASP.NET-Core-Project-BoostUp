namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BoostUp.Data.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static BoostUp.Areas.Admin.AdminConstants;

    public class AdministratorSeeder : ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    string adminEmail = configuration["Admin:Email"] ?? "TestAdminEmail";
                    string adminPassword = configuration["Admin:Password"] ?? "TestAdminPassword";

                    var admin = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Svilen",
                        LastName = "Stoev",
                        Gender = GenderType.Male,
                        DateOfBirth = new DateTime(1993, 12, 6),
                        ProfileImage = new Image
                        {
                            ImageUrl = "https://thumbs.dreamstime.com/b/man-inscription-admin-icon-outline-style-vector-web-design-isolated-white-background-179719942.jpg",
                        },
                        Address = new Address
                        {
                            Country = "Bulgaria",
                            City = "Sofia",
                            AddressText = "Gotse Delchev 108A",
                        }
                    };

                    var result = await userManager.CreateAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, role.Name);

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
