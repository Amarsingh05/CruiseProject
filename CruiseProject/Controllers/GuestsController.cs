using CruiseProject.Models;
using CruiseProject.ServiceRepository;
using CruiseProject.ServicesImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruiseProject.Controllers
{
    [Route("api/guests")]  
    [ApiController]
    public class GuestsController : BaseController
    {
        private readonly IGuestService _guestService;  

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        protected override async Task<IActionResult> ExecuteActionAsync(Func<Task<IActionResult>> action)
        {
            var result = await action();

            return result;
        }

        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves guests associated with a specific reservation number.
        /// </summary>
        /// <param name="reservationNumber">The reservation number for which to retrieve guests.</param>
        /// <returns>
        /// Returns a list of guests associated with the reservation number.
        /// </returns>
        /// <response code="200">Returns the list of guests.</response>
        /// <response code="400">If the reservation number is null or empty.</response>
        /// <response code="404">If no guests are found for the specified reservation number.</response>
        [HttpGet("GetGuests/{reservationNumber}")]    
        public Task<IActionResult> GetGuests(string reservationNumber) 
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(reservationNumber))  
                {
                    return BadRequest("Reservation Number cannot be null or empty");
                }

                var guests = await _guestService.GetGuestsAsync(reservationNumber);   

                if (guests == null || !guests.Any())
                {
                    return NotFound($"No guests found for the reservation number {reservationNumber}");
                }

                return Ok(guests);
            });
        }

        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves all details of a guest based on the specified person ID.
        /// </summary>
        /// <param name="personId">The unique identifier of the guest.</param>
        /// <returns>
        /// Returns the details of the guest identified by the person ID.
        /// </returns>
        /// <response code="200">Returns the details of the guest.</response>
        /// <response code="400">If the person ID is null or empty.</response>
        /// <response code="404">If no guest details are found for the specified person ID.</response>

        [HttpGet("GetAllDetailsOfGuest/{personId}")]
        public Task<IActionResult> GetAllDetailsOfGuest(string personId)   
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(personId))  
                {
                    return BadRequest("Person ID cannot be null or empty.");
                }

                var guestInfo = await _guestService.GetAllDetailsOfGuest(personId);

                if (guestInfo == null)
                {
                    return NotFound($"No guest details found for the Person ID {personId}");
                }

                return Ok(guestInfo);
            });
        }

        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves guests staying in a specific cabin number.
        /// </summary>
        /// <param name="cabinNumber">The number of the cabin to retrieve guests from.</param>
        /// <returns>
        /// Returns a list of guests staying in the specified cabin number.
        /// </returns>
        /// <response code="200">Returns the list of guests.</response>
        /// <response code="400">If the cabin number is less than 1.</response>
        /// <response code="404">If no guests are found for the specified cabin number.</response>
        [HttpGet("cabin/{cabinNumber}")]
        public Task<IActionResult> GetGuests(int cabinNumber)   
        {
            return HandleRequestAsync(async () =>
            {
                if (cabinNumber < 1)    
                {
                    return BadRequest("Cabin Number cannot be less than 1.");
                }

                var guests = await _guestService.GetGuestsAsync(cabinNumber);

                if (guests == null)
                {
                    return NotFound("No guests found for the cabin no");
                }

                return Ok(guests);
            });
        }
    }
}
