namespace BoostUp.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static BoostUp.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedIndustries(services);
            SeedEmploymentTypes(services);

            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                 new Category { Value = "1-10 employees" },
                 new Category { Value = "11-50 employees" },
                 new Category { Value = "51-200 employees" },
                 new Category { Value = "201-1,000 employees" },
                 new Category { Value = "1,001-10,000 employees" },
                 new Category { Value = "10,001+ employees" }
            });

            data.SaveChanges();
        }

        private static void SeedIndustries(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            if (data.Industries.Any())
            {
                return;
            }

            data.Industries.AddRange(new[]
            {
                 new Industry { Value = "Accounting" },
                 new Industry { Value = "Agriculture" },
                 new Industry { Value = "Airlines" },
                 new Industry { Value = "Attorneys/Law" },
                 new Industry { Value = "Automotive" },
                 new Industry { Value = "Construction" },
                 new Industry { Value = "Banking, Mortgage" },
                 new Industry { Value = "Education" },
                 new Industry { Value = "Energy" },
                 new Industry { Value = "Finance" },
                 new Industry { Value = "Food & Beverage" },
                 new Industry { Value = "Healthcare" },
                 new Industry { Value = "Real estate" },
                 new Industry { Value = "Media" },
                 new Industry { Value = "Production" },
                 new Industry { Value = "Retail" },
                 new Industry { Value = "Transportation" },
                 new Industry { Value = "Technology" },
                 new Industry { Value = "Other" }
            });

            data.SaveChanges();
        }

        private static void SeedEmploymentTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            if (data.EmploymentTypes.Any())
            {
                return;
            }

            data.EmploymentTypes.AddRange(new[]
            {
                 new EmploymentType { Value = "Intership" },
                 new EmploymentType { Value = "Volunteer" },
                 new EmploymentType { Value = "Temporary" },
                 new EmploymentType { Value = "Part-time" },
                 new EmploymentType { Value = "Full-time" },
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
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

                    const string adminEmail = "admin@boostup.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        Gender = GenderType.Unknown,
                        Address = new Address
                        {
                            Country = "Bulgaria",
                            City = "Sofia",
                        }
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
