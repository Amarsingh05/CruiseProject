using CruiseProject.Models;
using CruiseProject.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruiseProject.Controllers
{
    [Route("api/crews")]     
    [ApiController]
    public class CrewsController : BaseController
    {
        private readonly ICrewService _crew;    

        public CrewsController(ICrewService CrewService)
        {
            _crew = CrewService;
        }

        protected override async Task<IActionResult> ExecuteActionAsync(Func<Task<IActionResult>> action)
        {
            var result = await action();

            return result;
        }

        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves all details of crew members based on the specified person ID.
        /// </summary>
        /// <param name="personId">The unique identifier of the crew member.</param>
        /// <returns>
        /// Returns the details of the crew member identified by the person ID.
        /// </returns>
        /// <response code="200">Returns the details of the crew member.</response>
        /// <response code="400">If the person ID is null or empty.</response>
        /// <response code="404">If no crew member is found for the specified person ID.</response>

        [HttpGet("GetAllDetailsOfCrew/{personId}")]
        public Task<IActionResult> GetAllDetailsOfCrew(string personId)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(personId))
                {
                    return BadRequest("Person ID cannot be null or empty.");
                }

                var crewDetail = await _crew.GetAllDetailsOfCrew(personId);

                if (crewDetail == null)
                {
                    return NotFound($"No crew found for the Person ID {personId}");
                }

                return Ok(crewDetail);
            });
        }

        //---------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves the crew information for a specified department.
        /// </summary>
        /// <param name="departmentName">The name of the department for which to retrieve crew information.</param>
        /// <returns>
        /// Returns a list of crew information if the department name is valid and has crew members.
        /// Returns a BadRequest if the department name is null or empty.
        /// Returns a NotFound if no crew information is found for the specified department.
        /// </returns>
        /// <response code="200">Returns the list of crew information.</response>
        /// <response code="400">If the department name is null or empty.</response>
        /// <response code="404">If no crew information is found for the specified department.</response>
        
        [HttpGet("GetCrews/{departmentName}")]    
        public Task<IActionResult> GetCrews(string departmentName)
        {
            return HandleRequestAsync(async () =>
            {
                var crewInfo = await _crew.GetCrewsAsync(departmentName);

                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    return BadRequest("Department Name cannot be null or empty.");
                }

                if (crewInfo == null || crewInfo.Count == 0)
                {
                    return NotFound();
                }
                return Ok(crewInfo);
            });
        }
    }
}
