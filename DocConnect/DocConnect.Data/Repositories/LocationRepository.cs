using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {

        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CityNameAndIdDTO>> CityNamesSuggestionsAsync(string startingWith)
        {
            return await DbSet()
                .Where(l => l.CityName.Replace(" ", "").StartsWith(startingWith))
                .Select(l => new CityNameAndIdDTO
                {
                    Id = l.Id,
                    CityName = l.CityName
                })
                .ToListAsync();
        }
    }
}
