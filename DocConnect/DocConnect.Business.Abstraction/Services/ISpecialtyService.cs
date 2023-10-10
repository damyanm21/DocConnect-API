using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.Specialty;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a Service that provides operations related to Specialties.
    /// </summary>
    public interface ISpecialtyService
    {
        /// <summary>
        /// Retrieves a collection of all Specialty Entities as DTOs.
        /// </summary>
        /// <returns>An IEnumerable of SpecialtyGetDTO.</returns>
        Task<IEnumerable<SpecialtyGetDTO>> GetAllAsync();
    }
}

