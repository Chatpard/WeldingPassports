using Application.Interfaces.Repositories.SQL;
using Application.Requests.Admin;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WeldingPassportsApp.UnitTests.MockRepositories;
using Xunit;

namespace WeldingPassportsApp.UnitTests.Requests.Admin
{
    public class GetAdminDetailsRequestHandlerTests
    {
        [Fact]
        public async void Handle_ReturnsInvalidOperationExceptionForNonExistentAppSettings()
        {
            // Arrange
            var mockRepository = new Mock<IAppSettingsSQLRepository>();
            mockRepository.Setup(m => m.GetAllAppSettingsAsync()).ReturnsAsync((IEnumerable<AppSettings>) null);
            var mockMapper = new Mock<IMapper>();
            var handler = new GetAdminAppSettingsDetailsRequestHandler(mockRepository.Object, mockMapper.Object);

            var mockController = new Mock<Controller>();
            var request = new Mock<GetAdminAppSettingsDetailsRequest>(mockController.Object);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                // Act
                await handler.Handle(request.Object, It.IsAny<CancellationToken>());
            });
        }
    }
}
