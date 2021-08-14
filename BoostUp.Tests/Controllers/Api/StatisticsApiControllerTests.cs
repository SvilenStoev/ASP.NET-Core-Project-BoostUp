namespace BoostUp.Tests.Controllers.Api
{
    using BoostUp.Controllers.Api;
    using BoostUp.Tests.Mocks;
    using Xunit;

    public class StatisticsApiControllerTests
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.TotalCompanies);
            Assert.Equal(15, result.TotalJobs);
            Assert.Equal(20, result.TotalUsers);
            Assert.Equal(10, result.TotalRecruiters);
        }
    }
}
