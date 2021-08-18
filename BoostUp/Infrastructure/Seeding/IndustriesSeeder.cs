namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class IndustriesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration)
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
    }
}
