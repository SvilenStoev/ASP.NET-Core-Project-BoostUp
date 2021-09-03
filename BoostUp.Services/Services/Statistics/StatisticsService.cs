namespace BoostUp.Services.Statistics
{
    using System.Linq;
    using BoostUp.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly BoostUpDbContext data;

        public StatisticsService(BoostUpDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalCompanies = this.data.Companies.Where(c => c.IsPublic).Count();
            var totalJobs = this.data.Jobs.Count();
            var totalUsers = this.data.Users.Count();
            var totalRecruiters = this.data.Recruiters.Count();

            return new StatisticsServiceModel
            {
                TotalCompanies = totalCompanies,
                TotalJobs = totalJobs,
                TotalUsers = totalUsers,
                TotalRecruiters = totalRecruiters
            };
        }
    }
}
