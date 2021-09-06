namespace BoostUp.Tests.Services
{
    using System.Linq;

    using Xunit;
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using BoostUp.Tests.Mocks;
    using BoostUp.Services.Recruiters;

    public class RecruiterServiceTests
    {
        private const string UserId = "TestUserId";
        private const string RecruiterId = "TestRecruiterId";
        private const string RecruiterEmail = "recruiter@boostup.com";
        private const string RecruiterPhoneNumber = "0888888888";

        private readonly BoostUpDbContext data;
        private readonly Recruiter recruiter;

        public RecruiterServiceTests()
        {
            this.data = DatabaseMock.Instance;
            this.recruiter = new Recruiter { UserId = UserId, Id = RecruiterId };
        }

        [Fact]
        public void IsRecruiterShouldReturnTrueWhenUserIsRecruiter()
        {
            // Arrange
            var recruiterService = GetRecruiterService();

            // Act
            var result = recruiterService.IsRecruiter(UserId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRecruiterShouldReturnFalseWhenUserIsNotRecruiter()
        {
            // Arrange
            var recruiterService = GetRecruiterService();

            // Act
            var result = recruiterService.IsRecruiter("DifferentUserId");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IdByUserShouldReturnUserIdWhenUserIsRecruiter()
        {
            // Arrange
            var recruiterService = GetRecruiterService();

            // Act
            var result = recruiterService.IdByUser(UserId);

            // Assert
            Assert.Equal(RecruiterId, result);
        }

        [Fact]
        public void IdByUserShouldReturnNullWhenUserIsNotRecruiter()
        {
            // Arrange
            var recruiterService = GetRecruiterService();

            // Act
            var result = recruiterService.IdByUser("DifferentUserId");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateShouldAddRecruiterInTheDatabase()
        {
            //Arrange
            var recruiterService = GetRecruiterService();

            //Act
            recruiterService.Create(UserId, RecruiterEmail, RecruiterPhoneNumber);

            //Assert
            Assert.True(this.data.Recruiters.Any(r => r.Email == RecruiterEmail));
            Assert.True(this.data.Recruiters.Any(r => r.UserId == UserId));
            Assert.True(this.data.Recruiters.Any(r => r.PhoneNumber == RecruiterPhoneNumber));
        }

        private IRecruiterService GetRecruiterService()
        {
            this.data.Recruiters.Add(this.recruiter);
            this.data.SaveChanges();

            return new RecruiterService(this.data);
        }
    }
}
