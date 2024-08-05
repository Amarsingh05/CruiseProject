using CruiseProject.Models;
using System.Collections.Generic;

namespace SKO.CruiseProject.RepositoryLayer
{
    public interface ICrewRepository
    {
        public IQueryable<CrewInfo> GetCrewDetail();
        public IQueryable<DepartmentInfo> GetDepartmentDetail();
        public IQueryable<GuestCrewInfo> GetGuestCrewDetail();
    }
}
