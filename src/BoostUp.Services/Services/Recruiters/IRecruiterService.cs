namespace BoostUp.Services.Recruiters
{
    using BoostUp.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRecruiterService
    {
        void Create(
            string userId,
            string email,
            string phoneNumber);

        string IdByUser(string userId);

        bool IsRecruiter(string userId);
    }
}
