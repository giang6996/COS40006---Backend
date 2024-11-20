using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Account;
using Server.Presentation.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);

            // Mock Authorization Header for Controller's HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Bearer dummyAccessToken";
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Test]
        public async Task GetAccountProfile_AdminRole_WithAccountId_ReturnsAccount()
        {
            // Arrange
            var accountId = 1;
            var accountDto = new AccountDTO { Id = accountId, FirstName = "Admin", LastName = "User", Email = "admin@example.com" };
            _accountServiceMock.Setup(s => s.GetAccountByIdAsync(accountId)).ReturnsAsync(accountDto);

            var claims = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
            _controller.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var result = await _controller.GetAccountProfile(accountId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(accountDto));
        }

        [Test]
        public async Task GetAccountProfile_ResidentRole_NoAccountId_ReturnsOwnProfile()
        {
            // Arrange
            var accessToken = "dummyAccessToken";
            var residentProfile = new AccountDTO { Id = 2, FirstName = "Resident", LastName = "User", Email = "resident@example.com" };
            _accountServiceMock.Setup(s => s.GetAccountInfos(accessToken)).ReturnsAsync(residentProfile);

            var claims = new List<Claim> { new Claim(ClaimTypes.Role, "Resident") };
            _controller.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var result = await _controller.GetAccountProfile(null);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(residentProfile));
        }

        [Test]
        public async Task GetAccountProfile_NoAccountIdForAdmin_ReturnsBadRequest()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
            _controller.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var result = await _controller.GetAccountProfile(null);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult?.Value, Is.EqualTo("Account ID is required for Admin to view another user's profile."));
        }

        [Test]
        public async Task GetAllAccounts_AdminRole_ReturnsAllAccounts()
        {
            // Arrange
            var accounts = new List<AccountDTO>
            {
                new AccountDTO { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@example.com" },
                new AccountDTO { Id = 2, FirstName = "Resident", LastName = "User", Email = "resident@example.com" }
            };
            _accountServiceMock.Setup(s => s.GetAllAccountsAsync()).ReturnsAsync(accounts);

            var claims = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
            _controller.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var result = await _controller.GetAllAccounts();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(accounts));
        }

        [Test]
        public async Task ResetPassword_ValidRequest_ReturnsSuccessMessage()
        {
            // Arrange
            var updatePasswordRequest = new UpdatePasswordRequest { NewPassword = "NewPass123" };
            _accountServiceMock.Setup(s => s.UpdatePasswordAsync("dummyAccessToken", updatePasswordRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ResetPassword(updatePasswordRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo("Password reset successfully."));
        }

        [Test]
        public async Task ResetPassword_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var updatePasswordRequest = new UpdatePasswordRequest { NewPassword = "InvalidPassword" };
            _accountServiceMock.Setup(s => s.UpdatePasswordAsync("dummyAccessToken", updatePasswordRequest))
                               .ThrowsAsync(new Exception("Password reset failed"));

            // Act
            var result = await _controller.ResetPassword(updatePasswordRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult?.Value, Is.EqualTo("Failed to reset password: Password reset failed"));
        }
    }
}
