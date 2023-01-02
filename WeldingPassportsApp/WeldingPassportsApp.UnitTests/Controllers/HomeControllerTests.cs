using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using WeldingPassportsApp.Controllers;
using Xunit;

namespace WeldingPassportsApp.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        //[Fact]
        //public void Index_ReturnsAviewResult()
        //{
        //    // Arrange
        //    var mockLogger = new Mock<ILogger<HomeController>>();
        //    var controller = new HomeController(mockLogger.Object);

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    Assert.IsType<ViewResult>(result);
        //}

        [Fact]
        public void Privacy_ReturnsAviewResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);

            // Act
            var result = controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsAviewResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.TraceIdentifier = "20317";

            // Act
            var result = controller.Error();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SetLanguage_ReturnsALocalRedirectResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);

            string culture = "FR";

            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = controller.SetLanguage(culture, "/");

            // Assert
            Assert.IsType<LocalRedirectResult>(result);
        }
    }
}
