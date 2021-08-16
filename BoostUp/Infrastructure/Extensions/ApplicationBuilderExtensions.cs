namespace BoostUp.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using BoostUp.Data;
    using BoostUp.Infrastructure.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            var seeders = new List<ISeeder>
                          {
                              new CategoriesSeeder(),
                              new IndustriesSeeder(),
                              new EmploymentTypesSeeder(),
                              new CompaniesSeeder(),
                              new UsersSeeder(),
                              new AdministratorSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                seeder.Seed(services);
            }

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            data.Database.Migrate();
        }
    }
}
