using CruiseProject.Controllers;
using CruiseProject.Entities;
using CruiseProject.ServiceRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseProject.Tests.Controllers
{
    public class CrewControllerTest
    {
        private readonly Mock<ICrewService> _mockCrewService;   
        private readonly CrewsController _controller; 

        public CrewControllerTest()
        {
            _mockCrewService = new Mock<ICrewService>();
            _controller = new CrewsController(_mockCrewService.Object);
        }

        [Fact]
        public async Task GetCrewInfoByDepartment_ReturnsBadRequest_WhenDepartmentNameIsNullOrEmpty()
        {
            // Arrange
            string departmentName = null;

            // Act
            var result = await _controller.GetCrews(departmentName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Department Name cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetCrewInfoByDepartment_ReturnsNotFound_WhenNoCrewInfoFound()
        {
            // Arrange
            string departmentName = "Engineering";

            List<CrewInfoDto> crewInfoDtos = null;

            _mockCrewService.Setup(service => service.GetCrewsAsync(departmentName))
                .ReturnsAsync(crewInfoDtos);

            // Act
            var result = await _controller.GetCrews(departmentName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCrewInfoByDepartment_ReturnsOk_WithCrewInfo()
        {
            // Arrange
            string departmentName = "Engineering";

            var crewInfo = new List<CrewInfoDto>
        {
            new CrewInfoDto { Title = "Mr.", FirstName = "John", LastName = "Doe", SignOnDate = DateTime.Now, SignOffDate = DateTime.Now.AddMonths(6) }
        };

            _mockCrewService.Setup(service => service.GetCrewsAsync(departmentName))
                .ReturnsAsync(crewInfo);

            // Act
            var result = await _controller.GetCrews(departmentName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItems = Assert.IsType<List<CrewInfoDto>>(okResult.Value);
            Assert.Single(returnItems);
        }
    }
}
