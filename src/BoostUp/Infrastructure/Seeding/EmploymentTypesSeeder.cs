namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BoostUp.Data;
    using BoostUp.Data.Models;

    public class EmploymentTypesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration)
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
    }
}
