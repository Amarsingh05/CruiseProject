using CruiseProject.Entities;
using CruiseProject.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Esf;
using SKO.CruiseProject.RepositoryLayer;

namespace CruiseProject.ServiceRepository
{
    public class ShipService : IShipService
    {
        private readonly IShipRepository _shipRepository;
        public ShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        //------------------------------------------------------------------------------------------------------------------
        
        public async Task<ManifestDTO> GetActiveManifestAsync(string voyageNum)
        {
            var today = DateTime.Today;

            var voyage = await _shipRepository.GetVoyageInfos()
                .FirstOrDefaultAsync(v => v.VoyageNumber == voyageNum && v.IsVoyageActive == "Yes");
            
            if (voyage == null)
            {
                return null;
            }

            var guests = await (from g in _shipRepository.GetGuestInfos()
                                join gc in _shipRepository.GetGuestCrewInfos() on g.PersonId equals gc.PersonId
                                where g.VoyageNumber == voyageNum
                                select new GuestManifestDTO
                                {
                                    IsActive = g.IsActive,
                                    ReservationNumber = g.ReservationNumber,
                                    EmbarkDate = g.EmbarkDate,
                                    DebarkDate  = g.DbarkDate,
                                    FirstName = gc.FirstName,
                                    LastName = gc.LastName,
                                    IsOnBoard = gc.IsOnboard,
                                    IsCheckedIn = gc.IsCheckedIn,
                                    CheckedInTerminal = gc.CheckedinTerminal,
                                    GuestOrCrew = gc.GuestOrCrew,
                                    ProfileImage = gc.ProfileImage ?? string.Empty
                                }).ToListAsync();

            var crew = await (from c in _shipRepository.GetCrewInfos()
                              join gc in _shipRepository.GetGuestCrewInfos() on c.PersonId equals gc.PersonId
                              join d in _shipRepository.GetDepartmentInfos() on c.DeptId equals d.DeptId
                              join sd in _shipRepository.GetShipDepartmentConnections() on d.DeptId equals sd.DeptId
                              where c.SignOnDate <= today && c.SignOffDate >= today && sd.ShipId == voyage.ShipId
                              select new CrewManifestDTO
                              {
                                  DeptId = c.DeptId,
                                  SafetyNo = c.SafetyNo,
                                  SignOnDate = c.SignOnDate,
                                  SignOffDate = c.SignOffDate,
                                  FirstName = gc.FirstName,
                                  LastName = gc.LastName,
                                  IsOnBoard = gc.IsOnboard,
                                  IsCheckedIn = gc.IsCheckedIn,
                                  CheckedInTerminal = gc.CheckedinTerminal,
                                  GuestOrCrew = gc.GuestOrCrew,
                                  ProfileImage = gc.ProfileImage ?? string.Empty
                              }).ToListAsync();

            return new ManifestDTO
            {
                Guests = guests,
                Crew = crew,
            };
        }

        //------------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<ShipInfo>> GetAllShipsAsync()
        {
            return await _shipRepository.GetShipInfos().ToListAsync();
        }

        //------------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<VoyageInfo>> GetVoyagesAsync(string shipId)
        {
            return await _shipRepository.GetVoyageInfos().Where(v => v.ShipId == shipId).ToListAsync();
        }

        //------------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<GuestInfo>> GetGuestsAsync(string voyageNumber)
        {
            return await _shipRepository.GetGuestInfos().Where(g => g.VoyageNumber == voyageNumber).ToListAsync();
        }

        //------------------------------------------------------------------------------------------------------------------
        public async Task<List<string>> GetDepartmentsAsync(string shipId)
        {
            var connectionsQuery = from s in _shipRepository.GetShipDepartmentConnections()
                                   where s.ShipId == shipId
                                   select s;

            var connections = await connectionsQuery.ToListAsync();

            if (!connections.Any())
            {
                return new List<string>();
            }

            var departmentIds = connections.Select(c => c.DeptId).ToList();

            var departmentsQuery = from d in _shipRepository.GetDepartmentInfos()
                                   where departmentIds.Contains(d.DeptId)
                                   select d.Department;

            var departments = await departmentsQuery.ToListAsync();

            return departments;
        }

        //------------------------------------------------------------------------------------------------------------------
        public async Task<int> GetCountAsync(string person = null)
        {
            var query = _shipRepository.GetGuestCrewInfos().AsQueryable();

            if (!string.IsNullOrEmpty(person))
            {
                query = query.Where(p => p.GuestOrCrew.Contains(person));
            }

            return await query.CountAsync();
        }
    }
}
