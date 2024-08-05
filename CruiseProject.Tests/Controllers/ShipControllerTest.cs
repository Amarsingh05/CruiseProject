using CruiseProject.Controllers;
using CruiseProject.Models;

//using CruiseProject.Models;
using CruiseProject.ServiceRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CruiseProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CruiseProject.Tests.Controllers
{
    public class ShipControllerTest
    {
        private readonly Mock<IShipService> _mockShipService;   
        private readonly ShipsController _controller;   

        public ShipControllerTest()
        {
            _mockShipService = new Mock<IShipService>();
            _controller = new ShipsController(_mockShipService.Object);
        }

        // Tests for [HttpGet("manifest/{voyageNumber}")]
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetActiveManifest_ReturnsOkResult_WhenManifestExists()
        {
            // Arrange
            var voyageNumber = "VOY123";
            var manifest = new ManifestDTO { /* Initialize with test data */ };
            _mockShipService.Setup(service => service.GetActiveManifestAsync(voyageNumber)).ReturnsAsync(manifest);

            // Act
            var result = await _controller.GetActiveManifest(voyageNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedManifest = Assert.IsType<ManifestDTO>(okResult.Value);
            Assert.Equal(manifest, returnedManifest);
        }

        [Fact]
        public async Task Mocked_GetActiveManifest_ReturnsBadRequest_WhenVoyageNumberIsEmpty()
        {
            // Arrange
            string voyageNumber = "";

            // Act
            var result = await _controller.GetActiveManifest(voyageNumber);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voyage number cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task Mocked_GetActiveManifest_ReturnsNotFound_WhenManifestDoesNotExist()
        {
            // Arrange
            var voyageNumber = "NON_EXISTENT";
            _mockShipService.Setup(service => service.GetActiveManifestAsync(voyageNumber)).ReturnsAsync((ManifestDTO)null);

            // Act
            var result = await _controller.GetActiveManifest(voyageNumber);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No active voyage found for the given voyage number.", notFoundResult.Value);
        }

        //Tests for [HttpGet("GetAllShips")]
        //----------------------------------------------------------------------------------------------------
        [Fact] 
        public async Task Mocked_GetAllShips_ReturnsOkResult_WhenShipExist()
        {
            //Arrange
            var ships = new List<ShipInfo> { new ShipInfo(), new ShipInfo() };  
            _mockShipService.Setup(service => service.GetAllShipsAsync()).ReturnsAsync(ships); 

            //Act
            var result = await _controller.GetAllShips();   

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedShips = Assert.IsAssignableFrom<IEnumerable<ShipInfo>>(actionResult.Value); 
            Assert.Equal(2, returnedShips.Count());
        }

        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetAllShips_ReturnsNotFound_WhenNoShipsExist()
        {
            //Arrange
            _mockShipService.Setup(service => service.GetAllShipsAsync()).ReturnsAsync(new List<ShipInfo>());

            //Act
            var result = await _controller.GetAllShips();

            //Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No ships found.", actionResult.Value);
        }

        //tests for [HttpGet("GetAllVoyagesByShipName/{shipId}")]
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetAllVoyagesByShipId_ReturnsOkResult_WhenVoyagesExist()
        {
            var shipId = "ship1";
            var voyages = new List<VoyageInfo> { new VoyageInfo(), new VoyageInfo() };
            _mockShipService.Setup(service => service.GetVoyagesAsync(shipId)).ReturnsAsync(voyages);

            //Act
            var result = await _controller.GetVoyages(shipId);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedVoyages = Assert.IsAssignableFrom<IEnumerable<VoyageInfo>>(actionResult.Value);
            Assert.Equal(2, returnedVoyages.Count());
        }

        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetAllVoyagesByShipId_ReturnsBadRequest_WhenShipIdIsEmpty()
        {
            //Arrange --> would be empty here

            //Act
            var result = await _controller.GetVoyages("");

            //Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            //var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("Ship ID cannot be null or empty.", actionResult.Value);
        }

        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetAllVoyagesByShipID_ReturnsNotFound_WhenNoVoyagesExist()
        {
            //Arrange
            var shipId = "ship1";
            _mockShipService.Setup(service => service.GetVoyagesAsync(shipId)).ReturnsAsync(new List<VoyageInfo>());

            //Act
            var result = await _controller.GetVoyages(shipId);

            //Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No voyages found for ship ID {shipId}.", actionResult.Value);
        }

        //tests for [HttpGet("GetGuestsByVoyageNumber/{voyageNumber}")]
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetGuestsByVoyageNumber_ReturnOkResult_WhenGuestsExist()
        {
            //Arrange
            var voyageNumber = "voyage1";
            var guests = new List<GuestInfo> { new GuestInfo(), new GuestInfo() };
            _mockShipService.Setup(service => service.GetGuestsAsync(voyageNumber)).ReturnsAsync(guests);

            //Act
            var result = await _controller.GetGuests(voyageNumber);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGuests = Assert.IsAssignableFrom<IEnumerable<GuestInfo>>(okResult.Value);
            Assert.Equal(2, returnedGuests.Count());
        }

        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetGuestsByVoyageNumber_ReturnsBadRequest_WhenVoyageNumberIsEmpty()
        {
            //Arrange --> need not add anything here

            //Act
            var result = await _controller.GetGuests("");

            //Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voyage number cannot be null or empty.", actionResult.Value);
        }

        //----------------------------------------------------------------------------------------------------
        [Fact]
        public async Task Mocked_GetGuestsByVoyageNumber_ReturnsNotFound_WhenNoGuestsExist()
        {
            //Arrange
            var voyageNumber = "voyage1";
            _mockShipService.Setup(service => service.GetGuestsAsync(voyageNumber)).ReturnsAsync(new List<GuestInfo>());

            //Act
            var result = await _controller.GetGuests(voyageNumber);

            //Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No guests found for voyage number {voyageNumber}.", actionResult.Value);
        }

        //----------------------Unit test for Get all departments of ship by Ship Id-------------------------
        [Fact]        
        public async Task Mocked_GetDepartmentsByShipId_WhenDepartMentExists()
        {
            //Arrange
            string shipId = "CH";
            List<string> department = new List<string> { "Engineering", "Security", "FoodDept" };
            _mockShipService.Setup(service => service.GetDepartmentsAsync(shipId)).ReturnsAsync(department);

            //Act
            var result = await _controller.GetDepartments(shipId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItems = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(3, returnItems.Count);
        }

        [Fact]
        public async Task Mocked_GetDepartmentsByShipId_WhenNoDepartmentExists()
        {
            //Arrange
            string shipId = "N";
            List<string> department = null;
            _mockShipService.Setup(service => service.GetDepartmentsAsync(shipId)).ReturnsAsync(department);

            // Act
            var result = await _controller.GetDepartments(shipId);

            // Assert
            var notOkObject = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No departments found for the ship ID {shipId}.", notOkObject.Value);

        }
        [Fact]
        public async Task Mocked_GetDepartmentsByShipId_WhenShipIdIsEmpty()
        {
            //Act
            var result = await _controller.GetDepartments("");

            //Assert
            var badObject = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ship Id cannot be null or empty.", badObject.Value);
        }

        // Tests for [HttpGet("count/{personType}")]
        //----------------------------------------------------------------------------------------------------
        [Theory]
        [InlineData("guest", 100)]
        [InlineData("crew", 50)]
        public async Task Mocked_GetCount_ReturnsOkResult_WhenPersonTypeIsValid(string personType, int expectedCount)
        {
            // Arrange
            _mockShipService.Setup(service => service.GetCountAsync(personType)).ReturnsAsync(expectedCount);

            // Act
            var result = await _controller.GetCount(personType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(expectedCount, returnedCount);
        }

        [Theory]
        [InlineData("passenger")]
        [InlineData("staff")]
        [InlineData("")]
        public async Task Mocked_GetCount_ReturnsBadRequest_WhenPersonTypeIsInvalid(string personType)
        {
            // Act
            var result = await _controller.GetCount(personType);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Person Type must be either guest or crew.", badRequestResult.Value);
        }

        [Fact]
        public async Task Mocked_GetCount_ReturnsOkResult_WhenCountIsZero()
        {
            // Arrange
            string personType = "guest";
            int expectedCount = 0;
            _mockShipService.Setup(service => service.GetCountAsync(personType)).ReturnsAsync(expectedCount);

            // Act
            var result = await _controller.GetCount(personType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(expectedCount, returnedCount);
        }

        //-----------------------------------------End-------------------------------------------------------------

    }
}
