using CruiseProject.Entities;
using CruiseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseProject.ServicesImpl
{
    public interface IGuestService
    {
        public Task<IEnumerable<GuestDetailDto>> GetGuestsAsync(string reservationNumber);
        public Task<ActionResult<GuestInfo>> GetAllDetailsOfGuest(string personId);
        public Task<List<GuestCrewInfo>> GetGuestsAsync(int cabinNumber);

    }
}