using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CruiseProject.Models;
using SKO.CruiseProject.Models;
using CruiseProject.Entities;
using SKO.CruiseProject.RepositoryLayer;

namespace CruiseProject.ServiceRepository
{
    public class CrewService : ICrewService
    {
        public ICrewRepository _repository;

        public CrewService(ICrewRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<ActionResult<CrewInfo>> GetAllDetailsOfCrew(string person_id)
        {
            CrewInfo? crew = await _repository.GetCrewDetail().Where(crew => crew.PersonId == person_id).Include(c => c.Person).FirstOrDefaultAsync();

            return crew;
        }

        public async Task<List<CrewInfoDto>> GetCrewsAsync(string departmentName)
        {
            var crewInfo = await (from di in _repository.GetDepartmentDetail()
                                  join ci in _repository.GetCrewDetail() on di.DeptId equals ci.DeptId
                                  join gc in _repository.GetGuestCrewDetail() on ci.PersonId equals gc.PersonId
                                  where di.Department == departmentName
                                  select new CrewInfoDto
                                  {
                                      Title = gc.Title,
                                      FirstName = gc.FirstName,
                                      LastName = gc.LastName,
                                      SignOnDate = ci.SignOnDate,
                                      SignOffDate = ci.SignOffDate
                                  }).ToListAsync();

            return crewInfo;
        }
    }
}
