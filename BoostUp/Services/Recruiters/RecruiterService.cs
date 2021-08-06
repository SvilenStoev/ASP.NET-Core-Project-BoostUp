namespace BoostUp.Services.Recruiters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using BoostUp.Infrastructure;

    using BoostUp.Data;
    using BoostUp.Data.Models;

    public class RecruiterService : IRecruiterService
    {
        private readonly BoostUpDbContext data;

        public RecruiterService(BoostUpDbContext data)
            => this.data = data;

        public void Create(
            string userId,
            string email, 
            string phoneNumber)
        {
            var recruiterToAdd = new Recruiter
            {
                UserId = userId,
                Email = email,
                PhoneNumber = phoneNumber
            };

            this.data.Recruiters.Add(recruiterToAdd);

            this.data.SaveChanges();
        }

        public string IdByUser(string userId)
            => this.data
                .Recruiters
                .Where(r => r.UserId == userId)
                .Select(r => r.Id)
                .FirstOrDefault();

        public bool IsRecruiter(string userId)
            => this.data
                .Recruiters
                .Any(r => r.UserId == userId);
    }
}
