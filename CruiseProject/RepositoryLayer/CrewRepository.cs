using CruiseProject.Models;
using SKO.CruiseProject.Models;

namespace SKO.CruiseProject.RepositoryLayer
{
    public class CrewRepository:ICrewRepository
    {
        private readonly SkoTraineeContext _context;

        public CrewRepository(SkoTraineeContext context)
        {
            _context = context;
        }

        public IQueryable<CrewInfo> GetCrewDetail()
        {
            return _context.CrewInfos;
        }

        public IQueryable<DepartmentInfo> GetDepartmentDetail()
        {
            return _context.DepartmentInfos; 
        }

        public IQueryable<GuestCrewInfo> GetGuestCrewDetail()
        {
            return _context.GuestCrewInfos;
        }

    }
}
