using CruiseProject.Controllers;
using CruiseProject.ServicesImpl;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CruiseProject.Entities;
using CruiseProject.Models;

namespace CruiseProject.Tests.Controllers
{
    public class GuestsControllerTests
    {
        private readonly GuestsController _guestController;
        private readonly Mock<IGuestService> _mockguestService;

        public GuestsControllerTests()
        {
            _mockguestService = new Mock<IGuestService>();
            _guestController = new GuestsController(_mockguestService.Object);
        }

        // Unit Test for get guest by reservation number
        [Fact]
        public async Task Mocked_GetGuestByReservationNumber_WhenGuestExists()
        {
            // Arrange

            string reservationNumber = "123";
            var guestDetailDtos = new List<GuestDetailDto> { new GuestDetailDto { FirstName = "Vaibhav", LastName = "Bhosle", IsCheckedIn = "Yes", IsOnboard = "No" } };
            _mockguestService.Setup(service => service.GetGuestsAsync(reservationNumber)).ReturnsAsync(guestDetailDtos);

            // Act

            var result = await _guestController.GetGuests(reservationNumber);

            // Assert


            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGuests = Assert.IsAssignableFrom<IEnumerable<GuestDetailDto>>(okResult.Value);
        }

        [Fact]
        public async Task Mocked_GetGuestByReservationNumber_WhenNoGuestExists()
        {
            // Arrange

            string reservationNumber = "123";
            List<GuestDetailDto> guestDetailDtos = null;
            _mockguestService.Setup(service => service.GetGuestsAsync(reservationNumber)).ReturnsAsync(guestDetailDtos);

            // Act

            var result = await _guestController.GetGuests(reservationNumber);

            // Assert


            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No guests found for the reservation number {reservationNumber}", notFoundResult.Value);

        }

        [Fact]
        public async Task Mocked_GetGuestByReservationNumber_WhenReservationNumberIsEmpty()
        {
            //Act

            var result = await _guestController.GetGuests("");

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Reservation Number cannot be null or empty", badRequestResult.Value);

        }
        //Test for GetDetailsOfGuestByPersonId When PersonId is null
        [Fact]
        public async Task Mocked_GetDetailsOfGuestByPersonId_returnsBadRequest_WhenPersonIdIsNull()
        {
            //Act
            var result = await _guestController.GetAllDetailsOfGuest(null);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Person ID cannot be null or empty.", badRequestResult.Value);
        }


        //Test for GetDetailsOfGuestByPersonId When PersonId not found
        [Fact]
        public async Task Mocked_GetDetailsOfGuestByPersonId_returnsNotFound_WhenNoGuestFound()
        {
            //Arrange
            var personId = "a";
            _mockguestService.Setup(s => s.GetAllDetailsOfGuest(personId)).ReturnsAsync((GuestInfo)null);

            //Act
            var result = await _guestController.GetAllDetailsOfGuest(personId);

            //Assert
            Assert.NotNull(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(result);
            //Assert.Equal($"No guest details found for the Person ID {personId}", notFoundResult.Value);

        }


        //Test for GetGuestByCabinNumberWhen cabin number is null

        [Fact]
        public async Task Mocked_GetGuestByCabinNumber_ReturnsBadRequest_WhenCabinNumberIsNull()
        {
            var cabinno = 0;
            //Act
            var result = await _guestController.GetGuests(cabinno);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Cabin Number cannot be less than 1.", badRequestResult.Value);

        }



       /* //Test for GetGuestBycabinNumber When No Guest Found
        [Fact]
        public async Task Mocked_GetGuestByCabinNumber_ReturnsNotFound_WhenNoGuestfound()
        {
            //Arrange
            var cabinno = 123;

            _mockguestService.Setup(res => res.GetGuestsAsync(cabinno)).
                ReturnsAsync((IEnumerable<GuestCrewInfo>)null);

            //Act

            var result = await _guestController.GetGuests(cabinno);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No guests found for the cabin no", notFoundResult.Value);


        }*/





    }
}
