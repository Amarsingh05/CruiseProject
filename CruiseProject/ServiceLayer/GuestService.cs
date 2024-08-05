using CruiseProject.Entities;
using CruiseProject.Models;
using CruiseProject.ServicesImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKO.CruiseProject.Models;
using SKO.CruiseProject.RepositoryLayer;

namespace CruiseProject.ServiceRepository
{
    public class GuestService : IGuestService
    {

        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<IEnumerable<GuestDetailDto>> GetGuestsAsync(string reservationNumber)
        {
            var guestInfos = await _guestRepository.GetGuestInfosAsync();
            var filteredGuestInfos = guestInfos.Where(g => g.ReservationNumber == reservationNumber).ToList();

            if (!filteredGuestInfos.Any())
            {
                return new List<GuestDetailDto>();
            }

            var personIds = filteredGuestInfos.Select(g => g.PersonId).ToList();
            var guestCrewInfos = await _guestRepository.GetGuestCrewInfosAsync();
            var filteredGuestCrewInfos = guestCrewInfos.Where(gc => personIds.Contains(gc.PersonId)).ToList();

            var guestDetails = filteredGuestCrewInfos.Select(gc => new GuestDetailDto
            {
                FirstName = gc.FirstName,
                LastName = gc.LastName,
                IsCheckedIn = gc.IsCheckedIn,
                IsOnboard = gc.IsOnboard
            }).ToList();

            return guestDetails;
        }

        public async Task<ActionResult<GuestInfo>> GetAllDetailsOfGuest(string personId)
        {
            var guestInfos = await _guestRepository.GetGuestInfosAsync();
            var guest = guestInfos.FirstOrDefault(g => g.PersonId == personId);

            if (guest == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(guest);
        }

        public async Task<List<GuestCrewInfo>> GetGuestsAsync(int cabinNumber)
        {
            var guestCrewInfos = await _guestRepository.GetGuestCrewInfosAsync();
            return guestCrewInfos.Where(g => g.CabinNo == cabinNumber).ToList();
        }
    }
}
