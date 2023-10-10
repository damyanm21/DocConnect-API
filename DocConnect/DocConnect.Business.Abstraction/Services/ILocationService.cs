using DocConnect.Business.Models.LocationDTOs;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Service that provides functionality related to the locations
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Gets all city names that start with a substring
        /// </summary>
        /// <param name="startingWith">The starting with substring</param>
        /// <returns>An IEnumerable<CityNameAndIdDTO> containing ids and names of cities that matched the criteria</returns>
        Task<IEnumerable<CityNameAndIdDTO>> GetCitiesNamesSuggestionsAsync(string startingWith);
    }
}
