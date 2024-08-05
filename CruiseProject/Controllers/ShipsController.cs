using CruiseProject.Models;
using CruiseProject.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruiseProject.Controllers
{
    [Route("api/ships")]   
    [ApiController]
    [Authorize]
    public class ShipsController : BaseController   
    {
        private readonly IShipService _shipService;

        public ShipsController(IShipService shipService)
        {
            _shipService = shipService;
        }

        protected override async Task<IActionResult> ExecuteActionAsync(Func<Task<IActionResult>> action)
        {
            var result = await action();

            return result;
        }


        /// <summary>
        /// Retrieves the active manifest for a voyage specified by its voyage number.
        /// </summary>
        /// <param name="voyageNumber">The voyage number for which to retrieve the active manifest.</param>
        /// <returns>
        /// Returns the active manifest details for the specified voyage number.
        /// </returns>
        /// <response code="200">Returns the active manifest details.</response>
        /// <response code="400">If the voyage number is null or empty.</response>
        /// <response code="404">If no active voyage is found for the specified voyage number.</response>
        [Authorize(Roles = "admin")]
        [HttpGet("manifest/{voyageNumber}")]
        public Task<IActionResult> GetActiveManifest(string voyageNumber)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(voyageNumber))
                {
                    return BadRequest("Voyage number cannot be null or empty.");
                }

                var manifest = await _shipService.GetActiveManifestAsync(voyageNumber);

                if (manifest == null)
                {
                    return NotFound("No active voyage found for the given voyage number.");
                }

                return Ok(manifest);
            });
        }


        /// <summary>
        /// Retrieves all ships.
        /// </summary>
        /// <returns>
        /// Returns a list of all ships available.
        /// </returns>
        /// <response code="200">Returns the list of ships.</response>
        /// <response code="404">If no ships are found.</response>
        //------------------------------------------------------------------------------------------------
        [Authorize(Roles = "admin")]
        [HttpGet("GetAllShips")]
        public Task<IActionResult> GetAllShips()
        {
            return HandleRequestAsync(async () =>
            {
                var ships = await _shipService.GetAllShipsAsync();

                if (ships == null || !ships.Any())
                {
                    return NotFound("No ships found.");
                }

                return Ok(ships);
            });
        }


        /// <summary>
        /// Retrieves voyages associated with a specific ship ID.
        /// </summary>
        /// <param name="shipId">The ID of the ship for which to retrieve voyages.</param>
        /// <returns>
        /// Returns a list of voyages associated with the specified ship ID.
        /// </returns>
        /// <response code="200">Returns the list of voyages.</response>
        /// <response code="400">If the ship ID is null or empty.</response>
        /// <response code="404">If no voyages are found for the specified ship ID.</response>
        //------------------------------------------------------------------------------------------------
        [Authorize(Roles = "admin")]
        [HttpGet("GetVoyages/{shipId}")]   
        public Task<IActionResult> GetVoyages(string shipId)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(shipId))
                {
                    return BadRequest("Ship ID cannot be null or empty.");
                }

                var voyages = await _shipService.GetVoyagesAsync(shipId);

                if (voyages == null || !voyages.Any())
                {
                    return NotFound($"No voyages found for ship ID {shipId}.");
                }

                return Ok(voyages);
            });
        }


        /// <summary>
        /// Retrieves guests associated with a specific voyage number.
        /// </summary>
        /// <param name="voyageNumber">The voyage number for which to retrieve guests.</param>
        /// <returns>
        /// Returns a list of guests associated with the specified voyage number.
        /// </returns>
        /// <response code="200">Returns the list of guests.</response>
        /// <response code="400">If the voyage number is null or empty.</response>
        /// <response code="404">If no guests are found for the specified voyage number.</response>
        //------------------------------------------------------------------------------------------------
        [Authorize(Roles = "admin")]
        [HttpGet("GetGuests/{voyageNumber}")]
        public Task<IActionResult> GetGuests(string voyageNumber)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(voyageNumber))
                {
                    return BadRequest("Voyage number cannot be null or empty.");
                }

                var guests = await _shipService.GetGuestsAsync(voyageNumber);

                if (guests == null || !guests.Any())
                {
                    return NotFound($"No guests found for voyage number {voyageNumber}.");
                }

                return Ok(guests);
            });
        }


        /// <summary>
        /// Retrieves departments associated with a specific ship ID.
        /// </summary>
        /// <param name="shipId">The ID of the ship for which to retrieve departments.</param>
        /// <returns>
        /// Returns a list of department names associated with the specified ship ID.
        /// </returns>
        /// <response code="200">Returns the list of department names.</response>
        /// <response code="400">If the ship ID is null or empty.</response>
        /// <response code="404">If no departments are found for the specified ship ID.</response>
        //------------------------------------------------------------------------------------------------
        [Authorize(Roles = "admin")]
        [HttpGet("GetDepartments/{shipId}")]
        public Task<IActionResult> GetDepartments(string shipId)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(shipId))
                {
                    return BadRequest("Ship Id cannot be null or empty.");
                }

                var departmentNames = await _shipService.GetDepartmentsAsync(shipId);

                if (departmentNames == null || !departmentNames.Any())
                {
                    return NotFound($"No departments found for the ship ID {shipId}.");
                }

                return Ok(departmentNames);
            });            
        }


        /// <summary>
        /// Retrieves the count of either guests or crew members based on the specified person type.
        /// </summary>
        /// <param name="personType">The type of person ('guest' or 'crew') for which to retrieve the count.</param>
        /// <returns>
        /// Returns the count of persons based on the specified type.
        /// </returns>
        /// <response code="200">Returns the count of persons.</response>
        /// <response code="400">If the person type is neither 'guest' nor 'crew'.</response>
        //------------------------------------------------------------------------------------------------
        [Authorize(Roles = "admin")]
        [HttpGet("count/{personType}")]   
        public Task<IActionResult> GetCount(string personType)
        {
            return HandleRequestAsync(async () =>
            {
                if (personType.ToLower() != "guest" && personType.ToLower() != "crew")  
                {
                    return BadRequest("Person Type must be either guest or crew.");
                }

                var count = await _shipService.GetCountAsync(personType);

                return Ok(count);
            });
        }
    }
}
