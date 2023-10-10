using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Data.Abstraction.Repositories;
using System.Text.RegularExpressions;

namespace DocConnect.Business.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<CityNameAndIdDTO>> GetCitiesNamesSuggestionsAsync(string startingWith)
        {
            string textWithoutWhiteSpaces = Regex.Replace(startingWith, @"\s", string.Empty);

            var dtos = await _locationRepository.CityNamesSuggestionsAsync(textWithoutWhiteSpaces);

            return dtos;
        }
    }
}
