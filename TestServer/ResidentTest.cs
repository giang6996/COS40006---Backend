using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using NUnit.Framework;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Resident;
using Server.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class ResidentControllerTests
    {
        private Mock<IResidentService> _residentServiceMock;
        private ResidentController _controller;

        [SetUp]
        public void Setup()
        {
            _residentServiceMock = new Mock<IResidentService>();
            _controller = new ResidentController(_residentServiceMock.Object);

            // Set up a mock HttpContext
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Test]
        public async Task GetAllNewResidentRequest_ReturnsPendingAccounts()
        {
            // Arrange
            var pendingAccounts = new List<NewResidentResponse>
            {
                new NewResidentResponse { Email = "test1@example.com" },
                new NewResidentResponse { Email = "test2@example.com" }
            };
            _residentServiceMock.Setup(s => s.GetAllNewResidentRequest()).ReturnsAsync(pendingAccounts);

            // Act
            var result = await _controller.GetAllNewResidentRequest();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(pendingAccounts));
        }

        [Test]
        public async Task GetDetailsNewResidentRequest_ValidEmail_ReturnsAccountDetails()
        {
            // Arrange
            var email = "test@example.com";
            var accountDetails = new DetailsNewResidentResponse { AccountId = 1, Email = email };
            _residentServiceMock.Setup(s => s.GetDetailsNewResident(email)).ReturnsAsync(accountDetails);

            // Act
            var result = await _controller.GetDetailsNewResidentRequest(email);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(accountDetails));
        }

        [Test]
        public void DownloadDocument_FileExists_ReturnsFileContent()
        {
            // Arrange
            var documentLink = "1/test.pdf";

            // Encode the documentLink as the front end does
            var encodedDocumentLink = WebUtility.UrlEncode(documentLink);

            // Adjust the file path to target the correct folder in the main project
            var mainProjectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Server.Presentation"));
            var filePath = Path.Combine(mainProjectDirectory, "UploadedFiles", documentLink.Replace("/", Path.DirectorySeparatorChar.ToString()));

            var fileBytes = new byte[] { 1, 2, 3, 4 };
            var directoryPath = Path.GetDirectoryName(filePath);

            // Check if the directory path is valid and create it if it doesn't exist
            if (directoryPath == null)
            {
                throw new InvalidOperationException("Invalid directory path.");
            }

            Directory.CreateDirectory(directoryPath);

            // Verify that the directory was created successfully
            Assert.That(Directory.Exists(directoryPath), Is.True, "The directory was not created successfully.");

            // Write the file to the directory
            File.WriteAllBytes(filePath, fileBytes);

            // Verify that the file was created successfully
            Assert.That(File.Exists(filePath), Is.True, "The file was not created successfully in the specified directory.");

            // Act
            // Pass the encoded document link to simulate the front-end request
            var result = _controller.DownloadDocument(encodedDocumentLink);

            // Assert
            var fileResult = result as FileContentResult;
            Assert.That(fileResult, Is.Not.Null, "The file result should not be null.");
            Assert.That(fileResult?.FileContents, Is.EqualTo(fileBytes), "The file contents do not match.");
            Assert.That(fileResult?.ContentType, Is.EqualTo("application/octet-stream"), "The content type is not correct.");

            // Clean up
            File.Delete(filePath);
            Assert.That(File.Exists(filePath), Is.False, "The file was not deleted successfully.");
        }


        [Test]
        public void DownloadDocument_FileNotFound_ReturnsNotFound()
        {
            // Arrange
            var documentLink = "nonexistent.pdf";

            // Act
            var result = _controller.DownloadDocument(documentLink);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult?.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult?.Value, Is.EqualTo("Document not found"));
        }

        [Test]
        public async Task UpdateAccountStatus_ValidRequest_ReturnsSuccessMessage()
        {
            // Arrange
            var request = new UpdateAccountStatusRequest { AccountId = 1, Status = "Active" };
            _residentServiceMock.Setup(s => s.UpdateAccountStatus(request.AccountId, request.Status)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAccountStatus(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo("Account Status update successfully"));
        }

        [Test]
        public async Task DeleteAccount_ValidRequest_ReturnsSuccessMessage()
        {
            // Arrange
            var request = new DeleteResidentRequest { AccountId = 1 };
            _residentServiceMock.Setup(s => s.DeleteAccountAsync(request.AccountId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAccount(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo("Account and all related records deleted successfully."));
        }

        [Test]
        public async Task UpdateProfile_ValidRequest_ReturnsSuccessMessage()
        {
            // Arrange
            var request = new UpdateProfileRequest {PhoneNumber = "123123123"};
            _residentServiceMock.Setup(s => s.UpdateProfileAsync(It.IsAny<string>(), request)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProfile(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo("Profile updated successfully"));
        }
    }
}
