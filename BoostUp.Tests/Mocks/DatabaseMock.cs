namespace BoostUp.Tests.Mocks
{
    using System;
    using BoostUp.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static BoostUpDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<BoostUpDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new BoostUpDbContext(dbContextOptions);
            }
        }
    }
}
