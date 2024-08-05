using CruiseProject.Entities;
using CruiseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseProject.ServiceRepository
{
    public interface ICrewService
    {
        public Task<ActionResult<CrewInfo>> GetAllDetailsOfCrew(string person_id);
        Task<List<CrewInfoDto>> GetCrewsAsync(string departmentName);
    }
}
