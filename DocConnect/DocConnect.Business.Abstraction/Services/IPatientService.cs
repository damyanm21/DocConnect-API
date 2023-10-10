using DocConnect.Data.Models.Domains;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Contains the business logic related to the patients
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Async method to add a patient to the Database.
        /// </summary>
        /// <param name="patient">Patient Entity.</param>
        Task AddAsync(Patient patient);
    }
}