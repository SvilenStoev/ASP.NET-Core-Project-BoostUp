﻿namespace BoostUp.Infrastructure
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BoostUpDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            SeedIndustries(data);

            SeedEmploymentTypes(data);

            return app;
        }

        private static void SeedCategories(BoostUpDbContext data)
        {
            if (!data.Categories.Any())
            {
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

        private static void SeedIndustries(BoostUpDbContext data)
        {
            if (!data.Industries.Any())
            {
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
                     new Industry { Value = "Manufacturing" },
                     new Industry { Value = "Real estate" },
                     new Industry { Value = "Media" },
                     new Industry { Value = "Retail" },
                     new Industry { Value = "Transportation" },
                     new Industry { Value = "Technology" },
                     new Industry { Value = "Other" }
                });

                data.SaveChanges();
            }
        }

        private static void SeedEmploymentTypes(BoostUpDbContext data)
        {
            if (!data.EmploymentTypes.Any())
            {
                data.EmploymentTypes.AddRange(new[]
                {
                     new EmploymentType { Value = "Full-time" },
                     new EmploymentType { Value = "Part-time" },
                     new EmploymentType { Value = "Temporary" },
                     new EmploymentType { Value = "Volunteer" },
                     new EmploymentType { Value = "Intership" }
                });

                data.SaveChanges();
            }
        }
    }
}
