using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Business.Models.SpecialistDTOs;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;
using static DocConnect.Data.Models.Utilities.Constants.EnvironmentalVariablesConstants;
using static DocConnect.Data.Models.Utilities.Constants.DoctorConstants;
using static System.Net.WebRequestMethods;

namespace DocConnect.Data.Repositories
{
    public class SpecialistRepository : Repository<Doctor>, ISpecialistRepository
    {

        public SpecialistRepository(ApplicationDbContext context) : base(context)
        {
        }

        private readonly string ImageDomain = Environment.GetEnvironmentVariable(AzureImageDomain);

        public async Task<IEnumerable<SpecialistNamesAndIdDTO>> SpecialistsNamesSuggestionsAsync(string startingWith)
        {
            return await DbSet()
                 .Where(d => (d.FirstName + d.LastName).StartsWith(startingWith))
                 .Select(d => new SpecialistNamesAndIdDTO
                 {
                     Id = d.Id,
                     FirstName = d.FirstName,
                     LastName = d.LastName
                 })
                 .ToListAsync();
        }

        public async Task<IEnumerable<SpecialistFilteredInfoDTO>> FilterSpecialistsAsync(string? name, int? specialtyId, int? cityId)
        {
            return await DbSet()
                .Where(d =>
                    (name != null ? (d.FirstName + d.LastName).StartsWith(name) : true)
                    && (specialtyId != null ? d.SpecialtyId == specialtyId : true)
                    && (cityId != null ? d.LocationId == cityId : true)
                )
                .Select(d => new SpecialistFilteredInfoDTO
                {
                    Id = d.Id,
                    ImageUrl = string.Format(ImageUrlBuilder, ImageDomain, d.ImageUrl),
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    SpecialityName = d.Specialty.Name,
                    Address = string.Format(FullAddressBuilder, d.Address, d.Location.CityName),
                    Rating = d.Ratings.Any() ? d.Ratings.Average(r => r.RatingPoints) : 0
                })
                .ToListAsync();
        }

        public async Task<LocationLatitudeLongitudeDTO> GetDoctorsLocationDataAsync(int doctorId)
        {
            return await DbSet()
                .Where(d => d.Id == doctorId)
                .Select(d => new LocationLatitudeLongitudeDTO
                {
                    Latitude = d.Location.Latitude,
                    Longitude = d.Location.Longitude
                })
                .SingleAsync();      
        }

        public async Task<Doctor> GetSpecialistInfoByIdAsync(int id)
        {
            return await DbSet()
                .Where(d => d.Id == id)
                .Include(d => d.Specialty)
                .Include(d => d.Ratings)
                .Include(d => d.Location)
                .FirstOrDefaultAsync();
        }
    }
}
