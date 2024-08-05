using CruiseProject.Models;

namespace SKO.CruiseProject.RepositoryLayer
{
    public interface IShipRepository
    {
        IQueryable<VoyageInfo> GetVoyageInfos();
        IQueryable<GuestInfo> GetGuestInfos();
        IQueryable<GuestCrewInfo> GetGuestCrewInfos();
        IQueryable<CrewInfo> GetCrewInfos();
        IQueryable<DepartmentInfo> GetDepartmentInfos();
        IQueryable<ShipDepartmentConnection> GetShipDepartmentConnections();
        IQueryable<ShipInfo> GetShipInfos();
        Task<int> CountGuestCrewInfosAsync(IQueryable<GuestCrewInfo> query);
    }
}
