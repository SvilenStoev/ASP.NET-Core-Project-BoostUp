namespace BoostUp.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;
    
    using BoostUp.Data.Models;

    public class Users
    {
        public static IEnumerable<User> FiveUsers()
          => Enumerable.Range(0, 5).Select(i => new User());
    }
}
