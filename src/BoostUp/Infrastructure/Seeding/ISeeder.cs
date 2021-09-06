namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using Microsoft.Extensions.Configuration;

    public interface ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration);
    }
}
