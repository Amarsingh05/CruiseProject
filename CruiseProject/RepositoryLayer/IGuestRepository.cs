using CruiseProject.Models;

namespace SKO.CruiseProject.RepositoryLayer
{
    public interface IGuestRepository
    {
        Task<List<GuestInfo>> GetGuestInfosAsync();
        Task<List<GuestCrewInfo>> GetGuestCrewInfosAsync();

    }
}
