using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Account;
using Server.Models.ResponseModels;
using Server.Presentation.Controllers;
using System;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class LoginControllerTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private LoginController _controller;
        private Mock<ICookieHelper> _cookieHelperMock;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _cookieHelperMock = new Mock<ICookieHelper>();
            _controller = new LoginController(_accountServiceMock.Object, _cookieHelperMock.Object);

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsTokenAndSetsCookie()
        {
            // Arrange
            var request = new LoginRequest { Email = "user@example.com", Password = "ValidPassword123" };
            var token = new Token { AccessToken = "accessToken", RefreshToken = "refreshToken" };

            _accountServiceMock.Setup(s => s.LoginAsync(request)).ReturnsAsync(token);

            // Verify that SetCookie is called with the correct parameters
            _cookieHelperMock
                .Setup(c => c.SetCookie(
                    It.IsAny<HttpResponse>(),
                    "refreshToken",
                    token.RefreshToken,
                    It.Is<CookieOptions>(o => o.HttpOnly && o.Secure && o.SameSite == SameSiteMode.None && o.Expires.HasValue)))
                .Verifiable();

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(token));

            // Verify that the SetCookie method was called
            _cookieHelperMock.Verify();
        }

        [Test]
        public async Task Login_NullRequest_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Login(null);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));

            var actualMessage = badRequestResult?.Value?.GetType().GetProperty("message")?.GetValue(badRequestResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("Invalid login request."));
        }

        [Test]
        public async Task Login_AccountNotFound_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest { Email = "nonexistent@example.com", Password = "somePassword" };
            _accountServiceMock.Setup(s => s.LoginAsync(request)).ThrowsAsync(new Exception("Account not found"));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult, Is.Not.Null);
            Assert.That(unauthorizedResult?.StatusCode, Is.EqualTo(401));
            var actualMessage = unauthorizedResult?.Value?.GetType().GetProperty("message")?.GetValue(unauthorizedResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("Account not found."));
        }

        [Test]
        public async Task Login_IncorrectPassword_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest { Email = "user@example.com", Password = "wrongPassword" };
            _accountServiceMock.Setup(s => s.LoginAsync(request)).ThrowsAsync(new Exception("Incorrect Password"));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult, Is.Not.Null);
            Assert.That(unauthorizedResult?.StatusCode, Is.EqualTo(401));
            var actualMessage = unauthorizedResult?.Value?.GetType().GetProperty("message")?.GetValue(unauthorizedResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("Incorrect password."));
        }

        [Test]
        public async Task Login_InactiveAccount_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest { Email = "inactive@example.com", Password = "password" };
            _accountServiceMock.Setup(s => s.LoginAsync(request)).ThrowsAsync(new Exception("Account not active"));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult, Is.Not.Null);
            Assert.That(unauthorizedResult?.StatusCode, Is.EqualTo(401));
            var actualMessage = unauthorizedResult?.Value?.GetType().GetProperty("message")?.GetValue(unauthorizedResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("Account is not actived"));
        }

        [Test]
        public async Task Login_UnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var request = new LoginRequest { Email = "user@example.com", Password = "password" };
            _accountServiceMock.Setup(s => s.LoginAsync(request)).ThrowsAsync(new Exception("Some unexpected error"));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var errorResult = result as ObjectResult;
            Assert.That(errorResult, Is.Not.Null);
            Assert.That(errorResult?.StatusCode, Is.EqualTo(500));
            var actualMessage = errorResult?.Value?.GetType().GetProperty("message")?.GetValue(errorResult.Value, null) as string;
            Assert.That(actualMessage, Is.EqualTo("An error occurred while processing your request. Please try again later."));
        }
    }
}

