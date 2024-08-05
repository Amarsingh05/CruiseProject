using CruiseProject.Entities;
using CruiseProject.Models;

namespace CruiseProject.ServiceRepository
{
    public interface IShipService
    {
        Task<ManifestDTO> GetActiveManifestAsync(string voyageNumber);
        Task<IEnumerable<ShipInfo>> GetAllShipsAsync();
        Task<IEnumerable<VoyageInfo>> GetVoyagesAsync(string shipId);
        Task<IEnumerable<GuestInfo>> GetGuestsAsync(string voyageNumber);
        Task<List<string>> GetDepartmentsAsync(string shipId);
        Task<int> GetCountAsync(string person = null);
    }
}
