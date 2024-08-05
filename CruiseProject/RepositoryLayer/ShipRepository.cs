using CruiseProject.Models;
using Microsoft.EntityFrameworkCore;
using SKO.CruiseProject.Models;

namespace SKO.CruiseProject.RepositoryLayer
{
    public class ShipRepository : IShipRepository
    {
        private readonly SkoTraineeContext _context;

        public ShipRepository(SkoTraineeContext context)
        {
            _context = context;
        }
        
        public IQueryable<VoyageInfo> GetVoyageInfos() => _context.VoyageInfos;
        public IQueryable<GuestInfo> GetGuestInfos() => _context.GuestInfos;
        public IQueryable<GuestCrewInfo> GetGuestCrewInfos() => _context.GuestCrewInfos;
        public IQueryable<CrewInfo> GetCrewInfos() => _context.CrewInfos;
        public IQueryable<DepartmentInfo> GetDepartmentInfos() => _context.DepartmentInfos;
        public IQueryable<ShipDepartmentConnection> GetShipDepartmentConnections() => _context.ShipDepartmentConnections;
        public IQueryable<ShipInfo> GetShipInfos() => _context.ShipInfos;

        public Task<int> CountGuestCrewInfosAsync(IQueryable<GuestCrewInfo> query) => query.CountAsync();
    }
}
