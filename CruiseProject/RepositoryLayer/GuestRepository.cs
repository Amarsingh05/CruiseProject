using CruiseProject.Models;
using Microsoft.EntityFrameworkCore;
using SKO.CruiseProject.Models;

namespace SKO.CruiseProject.RepositoryLayer
{
    public class GuestRepository : IGuestRepository
    {
        private readonly SkoTraineeContext _context;

        public GuestRepository(SkoTraineeContext context)
        {
            _context = context;
        }

        public async Task<List<GuestInfo>> GetGuestInfosAsync()
        {
            return await _context.GuestInfos.ToListAsync();
        }

        public async Task<List<GuestCrewInfo>> GetGuestCrewInfosAsync()
        {
            return await _context.GuestCrewInfos.ToListAsync();
        }

    }
}
 
    

