using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Business.Models.SpecialistDTOs;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository that provides operations related to the doctor entity
    /// </summary>
    public interface ISpecialistRepository : IRepository<Doctor>
    {
        /// <summary>
        /// Gets all doctors whose names start with the passed string.
        /// </summary>
        /// <param name="startingWith"></param>
        /// <returns>An IEnumerable of SpecialistNamesAndIdDTO for the matched doctors</returns>
        Task<IEnumerable<SpecialistNamesAndIdDTO>> SpecialistsNamesSuggestionsAsync(string startingWith);

        /// <summary>
        /// Gets all doctors that match the criteria
        /// </summary>
        /// <param name="name">The combination of first and last name of the doctor</param>
        /// <param name="specialtyId">The Id of the speciality of the doctor</param>
        /// <param name="cityId">The id of the city of the doctor</param>
        /// <returns>An IEnumerable<FilteredDoctorInfoDTO>> representing a collection with all doctors that matched the criteria</returns>
        Task<IEnumerable<SpecialistFilteredInfoDTO>> FilterSpecialistsAsync(string? name, int? specialtyId, int? cityId);

        /// <summary>
        /// Gets the doctors location latitude and longitude
        /// </summary>
        /// <param name="doctorId">The id of the doctor whose location you want to get</param>
        /// <returns>LocationLatitudeLongitudeDTO containing 2 properties - latitude and longitude</returns>
        Task<LocationLatitudeLongitudeDTO> GetDoctorsLocationDataAsync(int doctorId);

        /// Gets a doctor by the corresponding id.
        /// </summary>
        /// <param id="id">The id of the doctor.</param>
        /// <returns></returns>
        Task<Doctor> GetSpecialistInfoByIdAsync(int id);
    }
}
