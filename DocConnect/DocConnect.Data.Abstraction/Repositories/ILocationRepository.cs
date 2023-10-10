using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// A repository interface providing data operations with the location entity 
    /// </summary>
    public interface ILocationRepository : IRepository<Location>
    {
        /// <summary>
        /// Gets all city names that start with a substring
        /// </summary>
        /// <param name="startingWith">The substring to check names by</param>
        /// <returns>An IEnumerable<CityNameAndIdDTO> containing all the matched city names</returns>
        Task<IEnumerable<CityNameAndIdDTO>> CityNamesSuggestionsAsync(string startingWith);
    }
}
