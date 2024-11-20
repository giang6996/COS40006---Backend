using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Server.BusinessLogic.Interfaces;
using Server.Common.DTOs;
using Server.Common.Enums;
using Server.Models.DTOs.Form;
using Server.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class FormControllerTests
    {
        private Mock<IFormService> _formServiceMock;
        private FormController _controller;

        [SetUp]
        public void Setup()
        {
            _formServiceMock = new Mock<IFormService>();
            _controller = new FormController(_formServiceMock.Object);

            // Mock Authorization Header
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Bearer dummyAccessToken";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Test]
        public async Task NewRequest_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new FormResidentRequest
            {
                Title = "Test Title",
                Type = "Request",
                Label = "Security",
                Description = "This is a test description"
            };
            _formServiceMock.Setup(s => s.HandleNewRequest(request, "dummyAccessToken")).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.NewRequest(request);

            // Assert
            var okResult = result as OkResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllRequest_ValidPermission_ReturnsAllRequests()
        {
            // Arrange
            var mockResponse = new List<FormResponse>
            {
                new FormResponse
                {
                    Id = 1,
                    Title = "Sample Complaint",
                    Type = "Complaint",
                    Label = "Maintenance",
                    Description = "Sample description",
                    Status = "Pending"
                }
            };
            _formServiceMock.Setup(s => s.HandleGetAllFormRequest("dummyAccessToken", Permission.CanViewAllForms, null, null, null)).ReturnsAsync(mockResponse);
            _controller.HttpContext.Items["UserPermission"] = Permission.CanViewAllForms;

            // Act
            var result = await _controller.GetAllRequest(null, null, null);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(mockResponse));
        }

        [Test]
        public async Task GetUserComplaints_ValidRequest_ReturnsUserComplaints()
        {
            // Arrange
            var mockResponse = new List<FormResponse>
            {
                new FormResponse
                {
                    Id = 1,
                    Title = "User Complaint",
                    Type = "Complaint",
                    Label = "Noise",
                    Description = "Noise complaint description",
                    Status = "Resolved"
                }
            };
            _formServiceMock.Setup(s => s.HandleGetUserComplaints("dummyAccessToken", null, null, null)).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.GetUserComplaints(null, null, null);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(mockResponse));
        }

        [Test]
        public async Task GetRequestDetail_ValidId_ReturnsRequestDetail()
        {
            // Arrange
            var formDetailResponse = new FormResponse
            {
                Id = 1,
                Title = "Request Detail",
                Type = "Request",
                Label = "Security",
                Description = "Request detail description",
                Status = "In Progress"
            };
            _formServiceMock.Setup(s => s.GetRequestDetail(1)).ReturnsAsync(formDetailResponse);

            // Act
            var result = await _controller.GetRequestDetail(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(formDetailResponse));
        }

        [Test]
        public async Task UpdateRequest_ValidRequest_ReturnsOk()
        {
            // Arrange
            var formUpdate = new FormUpdate { Response = "Updated response", Status = "Completed" };
            _formServiceMock.Setup(s => s.HandleAdminResponse(1, formUpdate.Response, formUpdate.Status)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateRequest(1, formUpdate);

            // Assert
            var okResult = result as OkResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }
    }
}
