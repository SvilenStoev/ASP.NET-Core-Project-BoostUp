namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BoostUp.Data;
    using BoostUp.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration)
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
    }
}
