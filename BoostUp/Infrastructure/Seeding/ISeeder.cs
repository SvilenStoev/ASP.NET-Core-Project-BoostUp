namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        public void Seed(IServiceProvider services);
    }
}
