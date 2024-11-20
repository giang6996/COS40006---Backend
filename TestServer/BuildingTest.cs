using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Presentation.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Server.Tests.Controllers
{
    [TestFixture]
    public class BuildingControllerTests
    {
        private Mock<IBuildingRepository> _buildingRepositoryMock;
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private BuildingController _controller;

        [SetUp]
        public void Setup()
        {
            _buildingRepositoryMock = new Mock<IBuildingRepository>();
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _controller = new BuildingController(_buildingRepositoryMock.Object, _apartmentRepositoryMock.Object);
        }

        [Test]
        public async Task SearchBuildings_WithQuery_ReturnsBuildings()
        {
            // Arrange
            var query = "Central";
            var buildings = new List<Building> { new Building { Id = 1, BuildingName = "Central Plaza" } }; // Replace `Building` with actual building type
            _buildingRepositoryMock.Setup(repo => repo.SearchBuildingsAsync(query)).ReturnsAsync(buildings);

            // Act
            var result = await _controller.SearchBuildings(query);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(buildings));
        }

        [Test]
        public async Task SearchBuildings_WithoutQuery_ReturnsAllBuildings()
        {
            // Arrange
            var buildings = new List<Building> { new Building { Id = 1, BuildingName = "Central Plaza" } }; // Replace `Building` with actual building type
            _buildingRepositoryMock.Setup(repo => repo.SearchBuildingsAsync("")).ReturnsAsync(buildings);

            // Act
            var result = await _controller.SearchBuildings();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(buildings));
        }

        [Test]
        public async Task SearchApartments_WithValidBuildingIdAndQuery_ReturnsApartments()
        {
            // Arrange
            long buildingId = 1;
            var query = "101";
            var apartments = new List<Apartment> { new Apartment { Id = 1, RoomNumber = 101 } }; // Replace `Apartment` with actual apartment type
            _apartmentRepositoryMock.Setup(repo => repo.SearchApartmentsAsync(buildingId, query)).ReturnsAsync(apartments);

            // Act
            var result = await _controller.SearchApartments(buildingId, query);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(apartments));
        }

        [Test]
        public async Task SearchApartments_WithInvalidBuildingId_ReturnsBadRequest()
        {
            // Arrange
            long invalidBuildingId = 0;
            var query = "101";

            // Act
            var result = await _controller.SearchApartments(invalidBuildingId, query);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult?.Value, Is.EqualTo("Invalid building ID."));
        }

        [Test]
        public async Task SearchApartments_WithoutQuery_ReturnsAllApartments()
        {
            // Arrange
            long buildingId = 1;
            var apartments = new List<Apartment> { new Apartment { Id = 1, RoomNumber = 101 } }; // Replace `Apartment` with actual apartment type
            _apartmentRepositoryMock.Setup(repo => repo.SearchApartmentsAsync(buildingId, "")).ReturnsAsync(apartments);

            // Act
            var result = await _controller.SearchApartments(buildingId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(apartments));
        }
    }
}
