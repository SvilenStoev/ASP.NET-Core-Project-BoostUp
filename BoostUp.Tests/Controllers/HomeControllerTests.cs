namespace BoostUp.Tests.Controllers
{
    using Xunit;
    using BoostUp.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class HomeControllerTests
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
           var homeController = new HomeController();

            //Act
           var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController();

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}