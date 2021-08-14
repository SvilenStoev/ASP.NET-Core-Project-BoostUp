namespace BoostUp.Tests.Mocks
{
    using BoostUp.Services.Statistics;
    using Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalCompanies = 10,
                        TotalJobs = 15,
                        TotalUsers = 20,
                        TotalRecruiters = 10
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
