using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeldingPassportsApp.Controllers;
using Xunit;

namespace WeldingPassportsApp.UnitTests.Controllers
{
    public class ContactsControllerTests
    {
        [Fact]
        public void Index_ReturnsAviewResult()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var env = new Mock<IWebHostEnvironment>();
            var mapper = new Mock<IMapper>();
            var controller = new CompanyContactsController(mediator.Object, env.Object, mapper.Object);
            string sortOrder = String.Empty;
            string currentFilter = string.Empty;
            string searchString = string.Empty;

            // Act
            var result = controller.Index(sortOrder, currentFilter, searchString, null);

            // Assert
            
            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
