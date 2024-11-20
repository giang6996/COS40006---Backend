using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Account;
using Server.Models.ResponseModels;
using Server.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class RegisterControllerTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private RegisterController _controller;
        private Mock<ICookieHelper> _cookieHelperMock;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _cookieHelperMock = new Mock<ICookieHelper>();
            _controller = new RegisterController(_accountServiceMock.Object, _cookieHelperMock.Object);

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Test]
        public async Task Register_ValidRequest_ReturnsTokenAndSetsCookie()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "newuser@example.com",
                Password = "StrongPassword123"
            };
            var documents = new List<IFormFile>(); // Empty list of documents for simplicity
            var token = new Token { AccessToken = "accessToken", RefreshToken = "refreshToken" };

            _accountServiceMock.Setup(s => s.RegisterAsync(request, documents)).ReturnsAsync(token);

            // Verify that the SetCookie method is called correctly
            _cookieHelperMock
                .Setup(c => c.SetCookie(
                    It.IsAny<HttpResponse>(),
                    "refreshToken",
                    token.RefreshToken,
                    It.Is<CookieOptions>(o => o.HttpOnly && o.Secure && o.SameSite == SameSiteMode.None && o.Expires.HasValue)))
                .Verifiable();

            // Act
            var result = await _controller.Register(request, documents);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(token));

            // Verify the cookie setup
            _cookieHelperMock.Verify();
        }

        [Test]
        public async Task Register_NullRequest_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Register(null, new List<IFormFile>());

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
            var actualMessage = badRequestResult?.Value?.GetType().GetProperty("message")?.GetValue(badRequestResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("Invalid register request."));
        }

        [Test]
        public async Task Register_InvalidOperation_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "duplicate@example.com",
                Password = "Password123"
            };
            var documents = new List<IFormFile>();
            _accountServiceMock
                .Setup(s => s.RegisterAsync(request, documents))
                .ThrowsAsync(new InvalidOperationException("Account already exists"));

            // Act
            var result = await _controller.Register(request, documents);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult?.Value, Is.EqualTo("Account already exists"));
        }

        [Test]
        public async Task Register_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "error@example.com",
                Password = "Password123"
            };
            var documents = new List<IFormFile>();
            _accountServiceMock.Setup(s => s.RegisterAsync(request, documents)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.Register(request, documents);

            // Assert
            var errorResult = result as ObjectResult;
            Assert.That(errorResult, Is.Not.Null);
            Assert.That(errorResult?.StatusCode, Is.EqualTo(500));
            var actualMessage = errorResult?.Value?.GetType().GetProperty("message")?.GetValue(errorResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("An error occurred while processing your request. Please try again later."));
        }
    }
}
