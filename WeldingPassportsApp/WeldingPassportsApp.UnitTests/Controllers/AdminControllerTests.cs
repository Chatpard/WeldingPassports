using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeldingPassportsApp.Controllers;
using Xunit;

namespace WeldingPassportsApp.UnitTests.Controllers
{
    public class AdminControllerTests
    {
        [Fact]
        public void AppSettingsDetails_ReturnsAIActionResult()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var env = new Mock<IWebHostEnvironment>();
            var controller = new AdminController(mediator.Object, env.Object);

            //Act
            var result = controller.AppSettingsDetails();

            //Assert
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void AppSettingsEdit_ReturnsAIActionResult()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var env = new Mock<IWebHostEnvironment>();
            var controller = new AdminController(mediator.Object, env.Object);

            //Act
            var result = controller.AppSettingsEdit(1);

            //Assert
            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
