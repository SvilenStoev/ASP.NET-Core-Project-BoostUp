namespace BoostUp.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BoostUp.Controllers;
    using BoostUp.Data.Models;
    using BoostUp.Services.Companies;
    using BoostUp.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

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
    }
}