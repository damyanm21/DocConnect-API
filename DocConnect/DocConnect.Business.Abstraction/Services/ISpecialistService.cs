using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.SpecialistDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Service that provides functionalities related to the doctors
    /// </summary>
    public interface ISpecialistService
    {
        /// <summary>
        /// Gets all doctors names that start with the passed string.
        /// </summary>
        /// <param name="startingWith">Criteria to match the names with</param>
        /// <returns>Task<IEnumerable<SpecialistNamesAndIdDTO>> contianing the names and Ids all doctors that matched </returns>
        Task<IEnumerable<SpecialistNamesAndIdDTO>> GetSpecialistsNamesSuggestionsAsync(string startingWith);

        /// <summary>
        /// Gets all doctors that match the criteria
        /// </summary>
        /// <param name="name">The combination of the first and last name of the doctor</param>
        /// <param name="specialtyId">The Id of the specialty of the doctor</param>
        /// <param name="cityId">The Id of the city of the doctor</param>
        /// <returns></returns>
        Task<IEnumerable<SpecialistFilteredInfoDTO>> FilterSpecialistsByCriteriaAsync(string? name, int? specialtyId, int? cityId);

        /// <summary>
        /// Gets a doctor by the corresponding id.
        /// </summary>
        /// <param id="id">The id of the doctor.</param>
        /// <returns></returns>
        Task<SpecialistDetailedInfoDTO> GetSpecialistByIdAsync(int id);
    }
}
