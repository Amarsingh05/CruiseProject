using CruiseProject.Entities;
using CruiseProject.Models;
using CruiseProject.ServiceRepository;
using CruiseProject.ServicesImpl;
using Moq;
using SKO.CruiseProject.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKO.CruiseProject.Tests.ServiceLayer
{
    public class GuestServiceTests
    {
        private readonly Mock<IGuestRepository> _mockGuestRepository;
        private readonly IGuestService _guestService;

        public GuestServiceTests()
        {
            _mockGuestRepository = new Mock<IGuestRepository>();
            _guestService = new GuestService(_mockGuestRepository.Object);
        }

        [Fact]
        public async Task Mocked_GetGuestsAsync_WhenGuestExists()
        {
            //Arrange
            string reservationNumber = "0393";

            var guestInfos = new List<GuestInfo>
        {
            new GuestInfo { PersonId = "1234", ReservationNumber = "0393", IsActive = "Yes", EmbarkDate = DateTime.Now.AddMonths(-3), DbarkDate = DateTime.Now.AddMonths(-4), SequenceNo = 13, VoyageNumber = "4563" },
            new GuestInfo { PersonId = "1235", ReservationNumber = "0393", IsActive = "Yes", EmbarkDate = DateTime.Now.AddMonths(-1), DbarkDate = DateTime.Now.AddMonths(-4), SequenceNo = 12, VoyageNumber = "4563" },
            new GuestInfo { PersonId = "1236", ReservationNumber = "0395", IsActive = "No", EmbarkDate = DateTime.Now.AddMonths(-2), DbarkDate = DateTime.Now.AddMonths(-4), SequenceNo = 23, VoyageNumber = "4563" },
            new GuestInfo { PersonId = "1237", ReservationNumber = "0396", IsActive = "Yes", EmbarkDate = DateTime.Now.AddMonths(-3), DbarkDate = DateTime.Now.AddMonths(-4), SequenceNo = 17, VoyageNumber = "4563" }
        };

            var guestCrewInfos = new List<GuestCrewInfo>
        {
            new GuestCrewInfo { PersonId = "1234", Title = "MRS", FirstName = "RINYAMI", LastName = "SHOKWUNGNAO", Dob = DateTime.Now.AddMonths(-3), Gender = "F", Nationality = "IND", IsOnboard = "YES", CabinNo = 37, IsCheckedIn = "YES", CheckedinTerminal = "C", GuestOrCrew = "CREW" },
            new GuestCrewInfo { PersonId = "1235", Title = "MR", FirstName = "JAMES", LastName = "LEE", Dob = DateTime.Now.AddMonths(-3), Gender = "F", Nationality = "IND", IsOnboard = "YES", CabinNo = 37, IsCheckedIn = "YES", CheckedinTerminal = "C", GuestOrCrew = "CREW" },
            new GuestCrewInfo { PersonId = "1236", Title = "MRS", FirstName = "ANNA", LastName = "KIM", Dob = DateTime.Now.AddMonths(-3), Gender = "F", Nationality = "IND", IsOnboard = "YES", CabinNo = 37, IsCheckedIn = "YES", CheckedinTerminal = "C", GuestOrCrew = "CREW" },
            new GuestCrewInfo { PersonId = "1237", Title = "MRS", FirstName = "MARIA", LastName = "GARCIA", Dob = DateTime.Now.AddMonths(-3), Gender = "F", Nationality = "IND", IsOnboard = "YES", CabinNo = 37, IsCheckedIn = "YES", CheckedinTerminal = "C", GuestOrCrew = "CREW" }
        };

            _mockGuestRepository.Setup(repo => repo.GetGuestInfosAsync()).ReturnsAsync(guestInfos);
            _mockGuestRepository.Setup(repo => repo.GetGuestCrewInfosAsync()).ReturnsAsync(guestCrewInfos);

            // Act
            var result = await _guestService.GetGuestsAsync(reservationNumber);

            // Assert
            Assert.NotNull(result);
            var guestDetails = result.ToList();
            Assert.Equal(2, guestDetails.Count);
            Assert.Equal("RINYAMI", guestDetails[0].FirstName);
            Assert.Equal("JAMES", guestDetails[1].FirstName);
        }

    }
}
