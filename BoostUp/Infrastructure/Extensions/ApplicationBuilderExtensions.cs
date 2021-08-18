namespace BoostUp.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BoostUp.Data;
    using BoostUp.Infrastructure.Seeding;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app, IConfiguration configuration)
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
                seeder.Seed(services, configuration);
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
